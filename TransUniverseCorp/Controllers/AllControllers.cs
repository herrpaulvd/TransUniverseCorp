using BL;
using Microsoft.AspNetCore.Mvc;

namespace TransUniverseCorp.Controllers
{
    [Controller]
    [Route("customer")]
    public class CustomerController : UniversalControllerBase<Customer>
    {
        public CustomerController()
            : base(rk => rk.CustomerRepo) { }
    }

    [Controller]
    [Route("driver")]
    public class DriverController : UniversalControllerBase<Driver>
    {
        public DriverController()
            : base(rk => rk.DriverRepo) { }
    }

    [Controller]
    [Route("edge")]
    public class EdgeController : UniversalControllerBase<Edge>
    {
        public EdgeController()
            : base(rk => rk.EdgeRepo) { }
    }

    [Controller]
    [Route("order")]
    public class OrderController : UniversalControllerBase<Order>
    {
        public OrderController()
            : base(rk => rk.OrderRepo) { }
    }

    [Controller]
    [Route("schedule")]
    public class ScheduleElementController : UniversalControllerBase<ScheduleElement>
    {
        public ScheduleElementController()
            : base(rk => rk.ScheduleElementRepo) { }
    }

    [Controller]
    [Route("spaceobject")]
    public class SpaceObjectController : UniversalControllerBase<SpaceObject>
    {
        public SpaceObjectController()
            : base(rk => rk.SpaceObjectRepo) { }
    }

    [Controller]
    [Route("spaceport")]
    public class SpacePortController : UniversalControllerBase<SpacePort>
    {
        public SpacePortController()
            : base(rk => rk.SpacePortRepo) { }
    }

    [Controller]
    [Route("spaceship")]
    public class SpaceshipController : UniversalControllerBase<Spaceship>
    {
        public SpaceshipController()
            : base(rk => rk.SpaceshipRepo) { }
    }

    [Controller]
    [Route("user")]
    public class UserController : UniversalControllerBase<User>
    {
        public UserController()
            : base(rk => rk.UserRepo) { }
    }
}
