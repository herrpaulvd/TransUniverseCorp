using BL;
using BL.Repos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TransUniverseCorp.Controllers
{
    [Route("/login")]
    public class LoginController : Controller
    {
        private IUserRepo UserRepo => RepoKeeper.Instance.UserRepo;

        private IActionResult WithError(string? error)
        {
            return View("LoginGet", error);
        }

        [Route("")]
        public IActionResult LoginGet()
        {
            return WithError(null);
        }

        [HttpPost]
        [Route("")]
        public IActionResult LoginPost(string? returnUrl)
        {
            var form = Request.Form;
            var login = form["login"];
            var password = form["password"];
            var user = UserRepo.FindByLogin(login);
            if (user is null)
                return WithError("User with the login does not exists");
            var hash = password.ToString().MakeHashCode();
            if (hash != user.PasswordHash)
                return WithError("Incorrect password");
            var roles = user.RolesAsString;
            List<Claim> claims = [new(ClaimsIdentity.DefaultNameClaimType, login)];
            if (roles.Contains('A'))
            {
                claims.Add(new(ClaimsIdentity.DefaultRoleClaimType, "admin"));
            }
            else if (roles.Contains('D'))
            {
                claims.Add(new(ClaimsIdentity.DefaultRoleClaimType, "driver"));
            }
            else if (roles.Contains('C'))
            {
                claims.Add(new(ClaimsIdentity.DefaultRoleClaimType, "customer"));
            }
            ClaimsIdentity identity = new(claims, "Cookies");
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new(identity)).Wait();
            return Redirect(returnUrl ?? "/");
        }
    }
}
