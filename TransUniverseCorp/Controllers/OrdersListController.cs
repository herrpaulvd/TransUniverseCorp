using BL.Repos;
using BL;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using TransUniverseCorp.Algorithm;
using Microsoft.Extensions.Hosting;
using TransUniverseCorp.Models;

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
        private IDriverRepo DriverRepo => RepoKeeper.Instance.DriverRepo;
        private ISpaceshipRepo SpaceshipRepo => RepoKeeper.Instance.SpaceshipRepo;
        private ISpaceObjectRepo SpaceObjectRepo => RepoKeeper.Instance.SpaceObjectRepo;
        private ISpacePortRepo SpacePortRepo => RepoKeeper.Instance.SpacePortRepo;

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
            int index = int.Parse(Request.Form["index"]!);
            lock(ObjectKeeper.Instance)
            {
                var model = (OrderModel)ObjectKeeper.Instance.Get(index)!;
                ObjectKeeper.Instance.Free(index);
                Order order = new()
                {
                    Customer = UserRepo.FindByLogin(User.FindFirst(ClaimsIdentity.DefaultNameClaimType)!.Value)!.Customer!.Value,
                    Driver = model.Driver.Id,
                    Spaceship = model.Spaceship.Id,
                    LoadingPort = model.LoadingPort.Id,
                    LoadingTime = model.LoadingTime,
                    UnloadingPort = model.UnloadingPort.Id,
                    UnloadingTime = model.UnloadingTime,
                    TotalCost = model.Cost,
                    TotalTime = model.TotalTime,
                    Volume = model.Volume,
                    Status = Order.STATUS_WIP
                };
                var first = model.ScheduleElements[0];
                first.Id = ScheduleElementRepo.Add(first);
                order.CurrentState = first.Id;
                order.Id = OrderRepo.Add(order);
                bool driverset = first.Driver is not null;
                bool spaceshipset = first.Spaceship is not null;
                if (driverset) model.Driver.CurrentState = first.Id;
                if(spaceshipset) model.Spaceship.CurrentState = first.Id;

                for(int i = 1; i < model.ScheduleElements.Count; i++)
                {
                    var curr = model.ScheduleElements[i];
                    first.Next = curr.Id = ScheduleElementRepo.Add(curr);
                    if(!driverset)
                    {
                        model.Driver.CurrentState = curr.Id;
                        driverset = true;
                    }
                    if(!spaceshipset)
                    {
                        model.Spaceship.CurrentState = curr.Id;
                        spaceshipset = true;
                    }
                    first = curr;
                }

                foreach(var e in model.ScheduleElements)
                {
                    e.Order = order.Id;
                    ScheduleElementRepo.Update(e);
                }
                DriverRepo.Update(model.Driver);
                SpaceshipRepo.Update(model.Spaceship);
                return Redirect("./");
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult NewOrder()
        {
            var form = Request.Form;
            var loadingPort = SpacePortRepo.FindByName(form["loading"]!);
            if (loadingPort is null) return IndexWithError("Invalid loading port");
            if (!DateTime.TryParse(form["ltime"]!, out var ltime))
                return IndexWithError("Invalid loading time");
            var unloadingPort = SpacePortRepo.FindByName(form["unloading"]!);
            if (loadingPort is null) return IndexWithError("Invalid unloading port");
            if (!DateTime.TryParse(form["utime"]!, out var utime))
                return IndexWithError("Invalid unloading time");
            if (!long.TryParse(form["volume"]!, out long volume))
                return IndexWithError("Invalid volume");

            lock(ObjectKeeper.Instance)
            {
                var (scheduleElements, cost, time, driver, spaceship) = PlanetGraph.GetBestWay(loadingPort, unloadingPort, ltime.Ticks, utime.Ticks, volume);
                if (scheduleElements is null || driver is null || spaceship is null)
                    return IndexWithError("Cannot make order: not enough time or free drivers/spaceships or destination is unreachable");
                if (scheduleElements.Count == 0)
                    return IndexWithError("The order is too primitive");
                OrderModel model = new()
                {
                    Cost = cost,
                    Driver = driver,
                    Spaceship = spaceship,
                    LoadingPort = loadingPort,
                    UnloadingPort = unloadingPort,
                    ScheduleElements = scheduleElements,
                    LoadingTime = ltime.Ticks,
                    UnloadingTime = utime.Ticks,
                    TotalTime = time,
                    Volume = volume
                };
                int index = ObjectKeeper.Instance.Keep(model);
                model.Index = index;
                return View("Commit", model);
            }
        }
    }
}
