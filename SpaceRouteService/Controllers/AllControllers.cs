using BaseAPI;
using BL;
using BL.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SpaceRouteService.Controllers
{
    [Authorize]
    [Controller]
    [Route("edge")]
    public class EdgeController : UniversalBaseAPIController<Edge>
    {
        public EdgeController()
            : base(rk => rk.EdgeRepo) { }
    }

    [Authorize]
    [Controller]
    [Route("order")]
    public class OrderController : UniversalBaseAPIController<Order>
    {
        public OrderController()
            : base(rk => rk.OrderRepo) { }

        [HttpGet]
        [Route("ordersbycustomer/{id:int}")]
        public IActionResult OrdersByCustomer(int? id)
        {
            if (id is null) return BadRequest();
            return PushArray(((IOrderRepo)Repo).GetOrdersByCustomer(id.Value).ToList());
        }
    }

    [Authorize]
    [Controller]
    [Route("schedule")]
    public class ScheduleElementController : UniversalBaseAPIController<ScheduleElement>
    {
        public ScheduleElementController()
            : base(rk => rk.ScheduleElementRepo) { }
    }

    [Authorize]
    [Controller]
    [Route("spaceobject")]
    public class SpaceObjectController : ExtendedBaseAPIController<SpaceObject>
    {
        public SpaceObjectController()
            : base(rk => rk.SpaceObjectRepo) { }
    }

    [Authorize]
    [Controller]
    [Route("spaceport")]
    public class SpacePortController : ExtendedBaseAPIController<SpacePort>
    {
        public SpacePortController()
            : base(rk => rk.SpacePortRepo) { }
    }
}
