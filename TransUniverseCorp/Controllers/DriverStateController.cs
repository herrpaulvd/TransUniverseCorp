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

        private int? GetDriverId()
        {
            return UserRepo.FindByLogin(User.FindFirst(ClaimsIdentity.DefaultNameClaimType)!.Value)!.Driver;
        }

        private Driver? GetDriverData()
        {
            var id = GetDriverId();
            if (id is null) return null;
            return DriverRepo.Get(id.Value)!;
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
            int? id = GetDriverId();
            if(id is null) return Redirect("../.?error=NOT%20A%20DRIVER");
            string port = Request.Form["port"]!;
            port = "p" + (port ?? "");
            using(HttpClient client = new())
            {
                HttpRequestMessage request = new(HttpMethod.Post, $"{ServiceAddress.Driver}/drvst/leave/{id.Value}/{port}");
                var response = client.SendAsync(request).Result;
                if(response is null)
                    return Redirect("../.?error=INVALID%20PORT");
                else
                    return Redirect("../");
            }
        }

        [HttpPost]
        [Route("next")]
        public IActionResult Next()
        {
            int? id = GetDriverId();
            if (id is null) return Redirect("../.?error=INVALID%20OPERATION");
            using (HttpClient client = new())
            {
                HttpRequestMessage request = new(HttpMethod.Post, $"{ServiceAddress.Driver}/drvst/next/{id.Value}");
                var response = client.SendAsync(request).Result;
                if (response is null)
                    return Redirect("../.?error=INVALID%20OPERATION");
                else
                    return Redirect("../");
            }
        }
    }
}
