using BL;
using BL.Repos;
using BaseAPI;

using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers
{
    [Controller]
    [Route("customer")]
    public class CustomerController : ExtendedBaseAPIController<Customer>
    {
        public CustomerController()
            : base(rk => rk.CustomerRepo) { }
    }
}
