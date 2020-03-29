using Commands.Customers;
using CommandsHandler.Customers;
using Infrastructure.Utilities.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerManagement.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICreateCustomerHandler _createCustomerHandler;


        public CustomerController(
            ICreateCustomerHandler createCustomerHandler)
        {
            _createCustomerHandler = createCustomerHandler;
        }

        [HttpPost]
        public async Task<Result> Post(CreateCustomer createCustomer)
        {
            var item  = HttpContext.Request;
            return await _createCustomerHandler.Handler(createCustomer);
        }

    }
}
