using Commands.Customers;
using CommandsHandler.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public void Get()
        {
            _createCustomerHandler.Handler(new CreateCustomer() 
            { LastName="reza",FirstName="jenabi",Email="jenabireza@gmail.com"  });
        }

    }
}
