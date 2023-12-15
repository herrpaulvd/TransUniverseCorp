using Microsoft.AspNetCore.Mvc;

namespace TransUniverseCorp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
