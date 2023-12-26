using BL.Repos;
using BL;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using TransUniverseCorp.Models;
using SharedModels;

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
        public IActionResult Index()
        {
            return IndexWithError(null);
        }

        [HttpPost]
        [Route("commit")]
        public IActionResult Commit()
        {
            string index = Request.Form["index"]!;
            using(HttpClient client = new())
            {
                HttpRequestMessage request = new(HttpMethod.Post, ServiceAddress.SpaceRoute + "/makeorder/commit/" + index);
                var response = client.SendAsync(request).Result;
                return Redirect("/orderlist");
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult NewOrder()
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

            using(HttpClient client = new())
            {
                HttpRequestMessage request = new(HttpMethod.Post, ServiceAddress.SpaceRoute + "/makeorder")
                {
                    Content = JsonContent.Create(new BuildOrderRequest()
                    {
                        LoadingPortName = loadingPortName,
                        LoadingTime = ltime.Ticks,
                        UnloadingPortName = unloadingPortName,
                        UnloadingTime = utime.Ticks,
                        Volume = volume,
                        Customer = UserRepo.FindByLogin(User.FindFirst(ClaimsIdentity.DefaultNameClaimType)!.Value)!.Customer!.Value
                    })
                };
                var response = client.SendAsync(request).Result;
                if (response.IsSuccessStatusCode)
                    return View("Commit", response.Content.ReadFromJsonAsync<ShortOrderModel>().Result);
                else
                    return IndexWithError("Cannot make order: invalid data or not enough time or free drivers/spaceships or destination is unreachable");
            }
        }
    }
}
