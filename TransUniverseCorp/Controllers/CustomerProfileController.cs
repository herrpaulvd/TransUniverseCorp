using BL.Repos;
using BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using UnviersalMV;

namespace TransUniverseCorp.Controllers
{
    [Authorize(Roles = "customer")]
    [Route("/csmprf")]
    public class CustomerProfileController : Controller
    {
        private IUserRepo UserRepo => RepoKeeper.Instance.UserRepo;
        private ICustomerRepo CustomerRepo => RepoKeeper.Instance.CustomerRepo;

        private IActionResult ShowWithError(CommonModel model, string? message = null)
        {
            return View("CommonUpdate", new UniversalViewBag(model, "csmprf", message));
        }

        private Customer? GetCustomerData()
        {
            var user = UserRepo.FindByLogin(User.FindFirst(ClaimsIdentity.DefaultNameClaimType)!.Value)!;
            if (user.Customer is null)
                return null;
            return CustomerRepo.Get(user.Customer.Value)!;
        }

        [Route("")]
        public IActionResult Index()
        {
            var customer = GetCustomerData();
            return customer is null ? Redirect("/login") : ShowWithError(new(customer));
        }

        [HttpPost]
        [Route("")]
        public IActionResult Post()
        {
            var customer = GetCustomerData();
            if (customer is null) return Redirect("/login");
            CommonModel model = new(customer);
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
                CustomerRepo.Update(customer);
                return ShowWithError(model);
            }
            else
                return ShowWithError(model, error.ToString());
        }
    }
}
