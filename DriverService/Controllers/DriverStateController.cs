using BL.Repos;
using BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DriverService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/drvst")]
    public class DriverStateController : Controller
    {
        private IDriverRepo DriverRepo => RepoKeeper.Instance.DriverRepo;
        private IScheduleElementRepo ScheduleElementRepo => RepoKeeper.Instance.ScheduleElementRepo;
        private IOrderRepo OrderRepo => RepoKeeper.Instance.OrderRepo;
        private ISpacePortRepo SpacePortRepo => RepoKeeper.Instance.SpacePortRepo;
        private ISpaceshipRepo SpaceshipRepo => RepoKeeper.Instance.SpaceshipRepo;

        private ScheduleElement? GetScheduleElement(Driver driver)
            => driver.CurrentState.HasValue ? ScheduleElementRepo.Get(driver.CurrentState.Value) : null;

        [HttpPost]
        [Route("leave/{id:int}/{portname}")]
        public IActionResult Leave(int? id, string? portname)
        {
            if (id is null || portname is null || portname.Length == 0 || portname[0] != 'p') return BadRequest();
            portname = portname[1..];
            var driver = DriverRepo.Get(id.Value);
            if (driver is null) return BadRequest();
            var current = GetScheduleElement(driver);
            if (current is not null && current.Order is not null)
            {
                var order = OrderRepo.Get(current.Order.Value)!;
                order.Status = Order.STATUS_FAILED;
                OrderRepo.Update(order);
            }
            SpacePort? port;
            if (portname == "")
            {
                driver.CurrentState = null;
            }
            else
            {
                port = SpacePortRepo.FindByName(portname);
                if (port is null)
                    return BadRequest();
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
            return Ok();
        }

        [HttpPost]
        [Route("next/{id:int}")]
        public IActionResult Next(int? id)
        {
            if (id is null) return BadRequest();
            var driver = DriverRepo.Get(id.Value);
            if(driver is null) return BadRequest();
            var current = GetScheduleElement(driver);
            if (current is null || current.Order is null)
                return BadRequest();
            var order = OrderRepo.Get(current.Order.Value)!;
            if (current.Next is null)
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
                if (next.Spaceship is not null)
                {
                    var spaceship = SpaceshipRepo.Get(next.Spaceship.Value)!;
                    spaceship.CurrentState = next.Id;
                    SpaceshipRepo.Update(spaceship);
                }
                DriverRepo.Update(driver);
            }
            OrderRepo.Update(order);
            return Ok();
        }
    }
}
