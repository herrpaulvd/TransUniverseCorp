using BL;
using BL.Repos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TransUniverseCorp.Controllers
{
    [Route("/register")]
    public class RegisterController : Controller
    {
        private IUserRepo UserRepo => RepoKeeper.Instance.UserRepo;
        private IDriverRepo DriverRepo => RepoKeeper.Instance.DriverRepo;
        private ICustomerRepo CustomerRepo => RepoKeeper.Instance.CustomerRepo;

        private IActionResult WithError(string? error)
        {
            return View("RegisterGet", error);
        }

        [Route("")]
        public IActionResult RegisterGet()
        {
            return WithError(null);
        }

        [HttpPost]
        [Route("")]
        public IActionResult RegisterPost(string? returnUrl)
        {
            var form = Request.Form;
            var login = form["login"];
            var password = form["password"];
            var repeat = form["repeat"];
            var roles = form["roles"].ToString();
            if (UserRepo.FindByLogin(login) is not null)
                return WithError("User with the login already exists");
            if (password != repeat)
                return WithError("Passwords differ");
            var user = new User();
            try
            {
                user.Login = login;
                user.PasswordHash = password.ToString().MakeHashCode();
                user.RolesAsString = roles;
            }
            catch (Exception)
            {
                return WithError("Invalid data");
            }
            List<Claim> claims = [new(ClaimsIdentity.DefaultNameClaimType, login)];
            if(roles.Contains('A'))
            {
                claims.Add(new(ClaimsIdentity.DefaultRoleClaimType, "admin"));
            }
            else if(roles.Contains('D'))
            {
                claims.Add(new(ClaimsIdentity.DefaultRoleClaimType, "driver"));
                Driver driver = new()
                {
                    Address = "",
                    Name = "",
                    Email = ""
                };
                user.Driver = DriverRepo.Add(driver);
            }
            else if(roles.Contains('C'))
            {
                claims.Add(new(ClaimsIdentity.DefaultRoleClaimType, "customer"));
                Customer customer = new()
                {
                    Address = "",
                    Email = "",
                    Name = ""
                };
                user.Customer = CustomerRepo.Add(customer);
            }
            UserRepo.Add(user);
            ClaimsIdentity identity = new(claims, "Cookies");
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new(identity)).Wait();
            return Redirect(returnUrl ?? "/");
        }
    }
}
