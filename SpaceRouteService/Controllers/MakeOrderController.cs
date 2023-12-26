using BL;
using BL.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SharedModels;
using SpaceRouteService.Models;
using System.Security.Claims;

namespace SpaceRouteService.Controllers
{
    [ApiController]
    [Route("/makeorder")]
    public class MakeOrderController : Controller
    {
        private IOrderRepo OrderRepo => RepoKeeper.Instance.OrderRepo;
        private IScheduleElementRepo ScheduleElementRepo => RepoKeeper.Instance.ScheduleElementRepo;
        private IDriverRepo DriverRepo => RepoKeeper.Instance.DriverRepo;
        private ISpaceshipRepo SpaceshipRepo => RepoKeeper.Instance.SpaceshipRepo;
        private ISpacePortRepo SpacePortRepo => RepoKeeper.Instance.SpacePortRepo;

        [HttpPost]
        [Route("")]
        public IActionResult MakeOrder(BuildOrderRequest request)
        {
            var loadingPort = SpacePortRepo.FindByName(request.LoadingPortName);
            if (loadingPort is null) return BadRequest();
            var unloadingPort = SpacePortRepo.FindByName(request.UnloadingPortName);
            if (unloadingPort is null) return BadRequest();

            lock (ObjectKeeper.Instance)
            {
                var (scheduleElements, cost, time, driver, spaceship) = PlanetGraph.GetBestWay(loadingPort, unloadingPort, request.LoadingTime, request.UnloadingTime, request.Volume);
                if (scheduleElements is null || driver is null || spaceship is null)
                    return BadRequest();
                if (scheduleElements.Count == 0)
                    return BadRequest();
                OrderModel model = new()
                {
                    Cost = cost,
                    Driver = driver,
                    Spaceship = spaceship,
                    LoadingPort = loadingPort,
                    UnloadingPort = unloadingPort,
                    ScheduleElements = scheduleElements,
                    LoadingTime = request.LoadingTime,
                    UnloadingTime = request.UnloadingTime,
                    TotalTime = time,
                    Volume = request.Volume,
                    Customer = request.Customer,
                };
                int index = ObjectKeeper.Instance.Keep(model);
                model.Index = index;
                ShortOrderModel shortModel = new()
                {
                    DriverName = model.Driver.Name,
                    SpaceshipName = model.Spaceship.Name,
                    Cost = cost,
                    Index = index
                };
                return Json(shortModel);
            }
        }

        [HttpPost]
        [Route("commit/{id:int}")]
        public IActionResult Commit(int? id)
        {
            if(id is null) return BadRequest();
            lock (ObjectKeeper.Instance)
            {
                var model = (OrderModel)ObjectKeeper.Instance.Get(id.Value)!;
                ObjectKeeper.Instance.Free(model.Index);
                Order order = new()
                {
                    Customer = model.Customer,
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
                if (spaceshipset) model.Spaceship.CurrentState = first.Id;

                for (int i = 1; i < model.ScheduleElements.Count; i++)
                {
                    var curr = model.ScheduleElements[i];
                    first.Next = curr.Id = ScheduleElementRepo.Add(curr);
                    if (!driverset)
                    {
                        model.Driver.CurrentState = curr.Id;
                        driverset = true;
                    }
                    if (!spaceshipset)
                    {
                        model.Spaceship.CurrentState = curr.Id;
                        spaceshipset = true;
                    }
                    first = curr;
                }

                foreach (var e in model.ScheduleElements)
                {
                    e.Order = order.Id;
                    ScheduleElementRepo.Update(e);
                }
                DriverRepo.Update(model.Driver);
                SpaceshipRepo.Update(model.Spaceship);
                return Ok();
            }
        }
    }
}
