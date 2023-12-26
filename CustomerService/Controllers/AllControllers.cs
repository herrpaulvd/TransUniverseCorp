using BL;
using BL.Repos;
using BaseAPI;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CustomerService.Controllers
{
    [Authorize]
    [Controller]
    [Route("customer")]
    public class CustomerController : ExtendedBaseAPIController<Customer>
    {
        public CustomerController()
            : base(rk => rk.CustomerRepo) { }
    }
}
