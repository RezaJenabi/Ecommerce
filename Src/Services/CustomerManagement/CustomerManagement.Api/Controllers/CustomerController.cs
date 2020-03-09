using Commands.Customers;
using CommandsHandler.Customers;
using Infrastructure.Utilities.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CustomerManagement.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICreateCustomerHandler _createCustomerHandler;


        public CustomerController(
            ILogger<CustomerController> logger,
            ICreateCustomerHandler createCustomerHandler)
        {
            _logger = logger;
            _createCustomerHandler = createCustomerHandler;
        }


        [HttpGet]
        public string Get()
        {
            return "Hello";
        }

        [HttpPost]
        public async Task<Result> PostAsync(CreateCustomer createCustomer)
        {
            return await _createCustomerHandler.Handler(createCustomer);
        }

    }
}
