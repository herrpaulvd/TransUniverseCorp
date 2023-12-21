using BL.Repos;
using BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransUniverseCorp.Models;
using System.Security.Claims;

namespace TransUniverseCorp.Controllers
{
    [Authorize(Roles = "driver")]
    [Route("/drvst")]
    public class DriverStateController : Controller
    {
        private IUserRepo UserRepo => RepoKeeper.Instance.UserRepo;
        private IDriverRepo DriverRepo => RepoKeeper.Instance.DriverRepo;
        private IScheduleElementRepo ScheduleElementRepo => RepoKeeper.Instance.ScheduleElementRepo;
        private IOrderRepo OrderRepo => RepoKeeper.Instance.OrderRepo;
        private ISpacePortRepo SpacePortRepo => RepoKeeper.Instance.SpacePortRepo;
        private ISpaceshipRepo SpaceshipRepo => RepoKeeper.Instance.SpaceshipRepo;

        private Driver? GetDriverData()
        {
            var user = UserRepo.FindByLogin(User.FindFirst(ClaimsIdentity.DefaultNameClaimType)!.Value)!;
            if (user.Driver is null)
                return null;
            return DriverRepo.Get(user.Driver.Value)!;
        }

        private ScheduleElement? GetScheduleElement(Driver driver)
            => driver.CurrentState.HasValue ? ScheduleElementRepo.Get(driver.CurrentState.Value) : null;

        private DriverFormData ConstructData(string? error)
        {
            var driver = GetDriverData()!;
            var currScheduleElement = GetScheduleElement(driver);
            return new()
            {
                CurrentScheduleElement = currScheduleElement,
                HasOrder = currScheduleElement?.Order is not null,
                Error = error
            };
        }

        [Route("")]
        public IActionResult Index(string? error)
        {
            return View(ConstructData(error));
        }

        [HttpPost]
        [Route("leave")]
        public IActionResult Leave()
        {
            var driver = GetDriverData()!;
            var current = GetScheduleElement(driver);
            if (current is not null && current.Order is not null)
            {
                var order = OrderRepo.Get(current.Order.Value)!;
                order.Status = Order.STATUS_FAILED;
                OrderRepo.Update(order);
            }

            string portname = Request.Form["port"]!;
            SpacePort? port;
            if(portname == "")
            {
                driver.CurrentState = null;
            }
            else
            {
                port = SpacePortRepo.FindByName(portname);
                if (port is null)
                    return Redirect("../.?error=INVALID%20PORT");
                ScheduleElement next = new()
                {
                    IsStop = true,
                    DepartureOrArrival = DateTime.Now.Ticks,
                    DestinationOrStop = port.Id,
                    Driver = driver.Id,
                    Spaceship = null,
                    Next = null,
                    Order = null,
                    PlannedDepartureOrArrival = null
                };
                driver.CurrentState = ScheduleElementRepo.Add(next);
            }
            DriverRepo.Update(driver);
            return Redirect("../");
        }

        [HttpPost]
        [Route("next")]
        public IActionResult Next()
        {
            var driver = GetDriverData()!;
            var current = GetScheduleElement(driver);
            if(current is null || current.Order is null)
                return Redirect("./.?error=INVALID%20OPERATION");
            var order = OrderRepo.Get(current.Order.Value)!;
            if(current.Next is null)
            {
                order.Status = Order.STATUS_DONE;
                ScheduleElement stop = new()
                {
                    DestinationOrStop = current.DestinationOrStop,
                    Driver = driver.Id,
                    Spaceship = current.Spaceship,
                    Order = null,
                    IsStop = true,
                };
                int stopIndex = ScheduleElementRepo.Add(stop);
                driver.CurrentState = stopIndex;
                var spaceship = SpaceshipRepo.Get(current.Spaceship!.Value)!;
                spaceship.CurrentState = stopIndex;
                DriverRepo.Update(driver);
                SpaceshipRepo.Update(spaceship);
            }
            else
            {
                var next = ScheduleElementRepo.Get(current.Next.Value)!;
                order.CurrentState = next.Id;
                driver.CurrentState = next.Id;
                if(next.Spaceship is not null)
                {
                    var spaceship = SpaceshipRepo.Get(next.Spaceship.Value)!;
                    spaceship.CurrentState = next.Id;
                    SpaceshipRepo.Update(spaceship);
                }
                DriverRepo.Update(driver);
            }
            OrderRepo.Update(order);
            return Redirect("../");
        }
    }
}
