using BL;
using BL.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using UnviersalMV;

namespace TransUniverseCorp.Controllers
{
    [Authorize(Roles = "driver")]
    [Route("/drvprf")]
    public class DriverProfileController : Controller
    {
        private IUserRepo UserRepo => RepoKeeper.Instance.UserRepo;
        private IDriverRepo DriverRepo => RepoKeeper.Instance.DriverRepo;

        private IActionResult ShowWithError(CommonModel model, string? message = null)
        {
            return View("CommonUpdate", new UniversalViewBag(model, "drvprf", message));
        }

        private Driver? GetDriverData()
        {
            var user = UserRepo.FindByLogin(User.FindFirst(ClaimsIdentity.DefaultNameClaimType)!.Value)!;
            if (user.Driver is null)
                return null;
            return DriverRepo.Get(user.Driver.Value)!;
        }

        [Route("")]
        public IActionResult Index()
        {
            var driver = GetDriverData();
            return driver is null ? Redirect("/login") : ShowWithError(new(driver));
        }

        [HttpPost]
        [Route("")]
        public IActionResult Post()
        {
            var driver = GetDriverData();
            if (driver is null) return Redirect("/login");
            CommonModel model = new(driver);
            StringBuilder error = new();
            foreach (var kv in Request.Form)
            {
                var p = kv.Key;
                var v = kv.Value.ToString();
                if (!model.SetValue(p, v))
                {
                    if (error.Length == 0) error.Append("Ошибка:\n");
                    error.Append($"Невозможно присвоить значение \"{v}\" полю {p}\n");
                }
            }
            if (error.Length == 0)
            {
                DriverRepo.Update(driver);
                return ShowWithError(model);
            }
            else
                return ShowWithError(model, error.ToString());
        }
    }
}
