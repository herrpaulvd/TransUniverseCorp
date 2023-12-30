using BL;
using BL.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using SharedModels;
using SpaceRouteService.Models;
using System.Security.Claims;

namespace SpaceRouteService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/makeorder")]
    public class MakeOrderController : Controller
    {
        private IOrderRepo OrderRepo => RepoKeeper.Instance.OrderRepo;
        private IScheduleElementRepo ScheduleElementRepo => RepoKeeper.Instance.ScheduleElementRepo;
        private IDriverRepo DriverRepo => RepoKeeper.Instance.DriverRepo;
        private ISpaceshipRepo SpaceshipRepo => RepoKeeper.Instance.SpaceshipRepo;

        public static Dictionary<long, int> id2objkeepindex = new();

        [HttpGet]
        [Route("askorder/{id:int}")]
        public IActionResult AskOrder(long? id)
        {
            if (id is null) return BadRequest();
            lock(ObjectKeeper.Instance)
            {
                lock(id2objkeepindex)
                {
                    int index = id2objkeepindex[id.Value];
                    var model = (OrderModel)ObjectKeeper.Instance.Get(index)!;
                    if (model.Ready)
                    {
                        ShortOrderModel shortModel = new()
                        {
                            DriverName = model.Driver.Name,
                            SpaceshipName = model.Spaceship.Name,
                            Cost = model.Cost,
                            Index = index,
                            Ready = true,
                            ID = id.Value,
                            Error = "",
                            Discardable = true,
                        };
                        return Json(shortModel);
                    }
                    else
                    {
                        ShortOrderModel shortModel = new()
                        {
                            DriverName = "",
                            SpaceshipName = "",
                            Cost = model.Cost,
                            Index = index,
                            Ready = false,
                            ID = id.Value,
                            Error = model.Discarded ? $"$\"Order #{id.Value} is discarded\"" : $"Order #{id.Value} is not ready",
                            Discardable = model.Discarded,
                        };
                        return Json(shortModel);
                    }
                }
            }
        }

        [HttpPost]
        [Route("commit/{id:int}")]
        public IActionResult Commit(int? id)
        {
            if(id is null) return BadRequest();
            lock (ObjectKeeper.Instance)
            {
                lock(id2objkeepindex)
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
                    id2objkeepindex.Remove(model.ID);
                    return Ok();
                }
            }
        }

        [HttpPost]
        [Route("discard/{id:int}")]
        public IActionResult Discard(int? id)
        {
            if (id is null) return BadRequest();
            lock (ObjectKeeper.Instance)
            {
                lock (id2objkeepindex)
                {
                    var model = (OrderModel)ObjectKeeper.Instance.Get(id.Value)!;
                    ObjectKeeper.Instance.Free(model.Index);
                    id2objkeepindex.Remove(model.ID);
                    return Ok();
                }
            }
        }
    }
}
