using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace TransUniverseCorp.Controllers
{
    [Route("/logout")]
    public class LogoutController : Controller
    {
        [Route("")]
        public IActionResult Post()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/login");
        }
    }
}
