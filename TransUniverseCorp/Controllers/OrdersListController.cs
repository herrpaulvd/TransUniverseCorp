using BL.Repos;
using BL;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using TransUniverseCorp.Models;
using SharedModels;
using IdentityModel.Client;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Timers;

namespace TransUniverseCorp.Controllers
{
    [Authorize(Roles = "customer")]
    [Route("/orderlist")]
    public class OrdersListController : Controller
    {
        private IUserRepo UserRepo => RepoKeeper.Instance.UserRepo;
        private ICustomerRepo CustomerRepo => RepoKeeper.Instance.CustomerRepo;
        private IOrderRepo OrderRepo => RepoKeeper.Instance.OrderRepo;
        private IScheduleElementRepo ScheduleElementRepo => RepoKeeper.Instance.ScheduleElementRepo;

        private Customer? GetCustomerData()
        {
            var user = UserRepo.FindByLogin(User.FindFirst(ClaimsIdentity.DefaultNameClaimType)!.Value)!;
            if (user.Customer is null)
                return null;
            return CustomerRepo.Get(user.Customer.Value)!;
        }

        private IActionResult IndexWithError(string? error)
        {
            var customer = GetCustomerData();
            var orders = OrderRepo.GetOrdersByCustomer(customer.Id);
            var model = orders.Select(order =>
            {
                string status;
                if (order.Status == Order.STATUS_FAILED)
                    status = "FAILED";
                else if (order.Status == Order.STATUS_DONE)
                    status = "DONE";
                else
                {
                    var current = ScheduleElementRepo.Get(order.CurrentState)!;
                    var what = current.IsStop ? "has rest at" : "is reaching";
                    status = $"WIP: driver {what} {current.DestinationOrStopPortName}, planned till {new DateTime(current.PlannedDepartureOrArrival!.Value)}";
                }
                return (order, status);
            }).ToList();
            if (error is not null)
                model.Insert(0, (null, error));

            return View("Index", model);
        }

        [Route("")]
        public IActionResult Index(string? error)
        {
            return IndexWithError(error);
        }

        private static HttpClientHandler MakeHandler()
            => new() { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true };

        private bool SetAccessToken(HttpClient mainClient)
        {
            using HttpClient client = new(MakeHandler());
            var disco = client.GetDiscoveryDocumentAsync(ServiceAddress.IdentityServer).Result;
            if (disco.IsError) return false;
            var tokenResponse = client.RequestClientCredentialsTokenAsync(new()
            {
                Address = disco.TokenEndpoint,
                ClientId = "mainAPP",
                ClientSecret = "superpupersecurepassword228",
                Scope = "allapi"
            }).Result;
            if (tokenResponse.IsError) return false;
            mainClient.SetBearerToken(tokenResponse.AccessToken!);
            return true;
        }

        private class UniqueIntKeper
        {
            private long id;
            private UniqueIntKeper() { }
            private static UniqueIntKeper Instance = new();
            public static long GetID()
            {
                lock(Instance) { return Instance.id++; }
            }
        }

        [HttpPost]
        [Route("commit")]
        public IActionResult Commit()
        {
            long ID = long.Parse(Request.Form["ID"]!);
            int index = int.Parse(Request.Form["index"]!);
            using(HttpClient client = new(MakeHandler()))
            {
                if (!SetAccessToken(client)) return Redirect("/orderlist?error=SERVICE%20UNAVAILABLE");
                HttpRequestMessage request = new(HttpMethod.Post, ServiceAddress.SpaceRoute + "/makeorder/commit/" + index);
                var response = client.SendAsync(request).Result;
                requests.Remove(ID);
                return Redirect("/orderlist/performedorders");
            }
        }

        [HttpPost]
        [Route("discard")]
        public IActionResult Discard()
        {
            long ID = long.Parse(Request.Form["ID"]!);
            int index = int.Parse(Request.Form["index"]!);
            using (HttpClient client = new(MakeHandler()))
            {
                if (!SetAccessToken(client)) return Redirect("/orderlist?error=SERVICE%20UNAVAILABLE");
                HttpRequestMessage request = new(HttpMethod.Post, ServiceAddress.SpaceRoute + "/makeorder/discard/" + index);
                var response = client.SendAsync(request).Result;
                requests.Remove(ID);
                return Redirect("/orderlist/performedorders");
            }
        }

        private static void Send<T>(T obj)
        {
            var factory = new ConnectionFactory()
            {
                HostName = ServiceAddress.QueueServer,
                Port = ServiceAddress.QueuePort,
                UserName = ServiceAddress.QueueUser,
                Password = ServiceAddress.QueuePassword,
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: ServiceAddress.QueueName,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
            string message = JsonConvert.SerializeObject(obj);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                    routingKey: ServiceAddress.QueueName,
                                    basicProperties: null,
                                    body: body);
        }

        private static Dictionary<long, BuildOrderRequest> requests = new();

        [HttpPost]
        [Route("")]
        public IActionResult NewOrder()
        {
            lock(requests)
            {
                var form = Request.Form;
                string loadingPortName = form["loading"]!;
                if (!DateTime.TryParse(form["ltime"]!, out var ltime))
                    return IndexWithError("Invalid loading time");
                string unloadingPortName = form["unloading"]!;
                if (!DateTime.TryParse(form["utime"]!, out var utime))
                    return IndexWithError("Invalid unloading time");
                if (!long.TryParse(form["volume"]!, out long volume))
                    return IndexWithError("Invalid volume");
                BuildOrderRequest r = new()
                {
                    LoadingPortName = loadingPortName,
                    LoadingTime = ltime.Ticks,
                    UnloadingPortName = unloadingPortName,
                    UnloadingTime = utime.Ticks,
                    Volume = volume,
                    Customer = UserRepo.FindByLogin(User.FindFirst(ClaimsIdentity.DefaultNameClaimType)!.Value)!.Customer!.Value,
                    ID = UniqueIntKeper.GetID()
                };
                Send(r);
                requests.Add(r.ID, r);
                return Redirect("/orderlist/performedorders");
            }
        }

        [HttpGet]
        [Route("performedorders")]
        public IActionResult GetPerformedOrders()
        {
            int customer = UserRepo.FindByLogin(User.FindFirst(ClaimsIdentity.DefaultNameClaimType)!.Value)!.Customer!.Value;
            lock (requests)
            {
                List<ShortOrderModel> toview = new();
                foreach(var kv in requests)
                {
                    if (kv.Value.Customer != customer) continue;
                    using HttpClient client = new(MakeHandler());
                    if(!SetAccessToken(client)) return Redirect("/orderlist?error=SERVICE%20UNAVAILABLE");
                    HttpRequestMessage request = new(HttpMethod.Get, $"{ServiceAddress.SpaceRoute}/makeorder/askorder/{kv.Key}");
                    var response = client.SendAsync(request).Result;
                    ShortOrderModel model;
                    if(response.IsSuccessStatusCode)
                    {
                        model = response.Content.ReadFromJsonAsync<ShortOrderModel>().Result!;
                    }
                    else
                    {
                        model = new ShortOrderModel()
                        {
                            DriverName = "",
                            SpaceshipName = "",
                            Error = $"Order #{kv.Key} is in queue",
                            ID = kv.Key,
                            Discardable = false,
                        };
                    }
                    toview.Add(model);
                }
                return View("PerformedOrders", toview);
            }
        }
    }
}
