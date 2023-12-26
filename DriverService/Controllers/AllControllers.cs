using BL;
using BL.Repos;
using BaseAPI;

using Microsoft.AspNetCore.Mvc;

namespace DriverService.Controllers
{
    [Controller]
    [Route("driver")]
    public class DriverController : ExtendedBaseAPIController<Driver>
    {
        public DriverController()
            : base(rk => rk.DriverRepo) { }
    }

    [Controller]
    [Route("spaceship")]
    public class SpaceshipController : ExtendedBaseAPIController<Spaceship>
    {
        public SpaceshipController()
            : base(rk => rk.SpaceshipRepo) { }
    }
}
