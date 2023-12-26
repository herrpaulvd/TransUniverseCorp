using BL;
using BL.Repos;
using BaseAPI;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace UserService.Controllers
{
    [Authorize]
    [Controller]
    [Route("user")]
    public class UserController : UniversalBaseAPIController<User>
    {
        public UserController()
            : base(rk => rk.UserRepo) { }

        [HttpGet]
        [Route("findbylogin/{login}")]
        public IActionResult FindByLogin(string? login)
        {
            if (login is null) return NotFound();
            try
            {
                return Push(((IUserRepo)Repo).FindByLogin(login));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
