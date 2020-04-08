using CustomerManagement.Commands.Customers;
using CustomerManagement.CommandsHandler.Customers;
using Infrastructure.Utilities.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerManagement.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
   // [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICreateCustomerHandler _createCustomerHandler;


        public CustomerController(
            ICreateCustomerHandler createCustomerHandler)
        {
            _createCustomerHandler = createCustomerHandler;
        }

        [HttpGet]
        public string Get()
        {
            var item = User;
            return "OK";
        }

        [HttpPost]
        public async Task<Result> Post(CreateCustomer createCustomer)
        {
            return await _createCustomerHandler.Handler(createCustomer);
        }

    }
}
