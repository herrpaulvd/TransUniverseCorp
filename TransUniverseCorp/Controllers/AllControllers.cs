using Entities;
using Microsoft.AspNetCore.Mvc;

namespace TransUniverseCorp.Controllers
{
    [Controller]
    [Route("customer")]
    public class CustomerController : UniversalControllerBase<Customer>
    {
        public CustomerController(TransUniverseDbContext context)
            : base(context, context.Customers, i => context.Customers.First(c => c.Id == i), c => c.Id) { }
    }

    [Controller]
    [Route("driver")]
    public class DriverController : UniversalControllerBase<Driver>
    {
        public DriverController(TransUniverseDbContext context)
            : base(context, context.Drivers, i => context.Drivers.First(c => c.Id == i), c => c.Id) { }
    }

    [Controller]
    [Route("edge")]
    public class EdgeController : UniversalControllerBase<Edge>
    {
        public EdgeController(TransUniverseDbContext context)
            : base(context, context.Edges, i => context.Edges.First(c => c.Id == i), c => c.Id) { }
    }

    [Controller]
    [Route("order")]
    public class OrderController : UniversalControllerBase<Order>
    {
        public OrderController(TransUniverseDbContext context)
            : base(context, context.Orders, i => context.Orders.First(c => c.Id == i), c => c.Id) { }
    }

    [Controller]
    [Route("schedule")]
    public class ScheduleElementController : UniversalControllerBase<ScheduleElement>
    {
        public ScheduleElementController(TransUniverseDbContext context)
            : base(context, context.ScheduleElements, i => context.ScheduleElements.First(c => c.Id == i), c => c.Id) { }
    }

    [Controller]
    [Route("spaceobject")]
    public class SpaceObjectController : UniversalControllerBase<SpaceObject>
    {
        public SpaceObjectController(TransUniverseDbContext context)
            : base(context, context.SpaceObjects, i => context.SpaceObjects.First(c => c.Id == i), c => c.Id) { }
    }

    [Controller]
    [Route("spaceport")]
    public class SpacePortController : UniversalControllerBase<SpacePort>
    {
        public SpacePortController(TransUniverseDbContext context)
            : base(context, context.SpacePorts, i => context.SpacePorts.First(c => c.Id == i), c => c.Id) { }
    }

    [Controller]
    [Route("spaceship")]
    public class SpaceshipController : UniversalControllerBase<Spaceship>
    {
        public SpaceshipController(TransUniverseDbContext context)
            : base(context, context.Spaceships, i => context.Spaceships.First(c => c.Id == i), c => c.Id) { }
    }

    [Controller]
    [Route("user")]
    public class UserController : UniversalControllerBase<User>
    {
        public UserController(TransUniverseDbContext context)
            : base(context, context.Users, i => context.Users.First(c => c.Id == i), c => c.Id) { }
    }
}
