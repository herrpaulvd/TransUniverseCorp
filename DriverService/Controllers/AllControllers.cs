using BL;
using BL.Repos;
using BaseAPI;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DriverService.Controllers
{
    [Authorize]
    [Controller]
    [Route("driver")]
    public class DriverController : ExtendedBaseAPIController<Driver>
    {
        public DriverController()
            : base(rk => rk.DriverRepo) { }
    }

    [Authorize]
    [Controller]
    [Route("spaceship")]
    public class SpaceshipController : ExtendedBaseAPIController<Spaceship>
    {
        public SpaceshipController()
            : base(rk => rk.SpaceshipRepo) { }
    }
}
