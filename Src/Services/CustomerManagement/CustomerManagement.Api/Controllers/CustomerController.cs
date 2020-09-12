using CustomerManagement.Commands.Customers;
using CustomerManagement.CommandsHandler.Customers;
using Infrastructure.Core.Security;
using Infrastructure.Utilities.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerManagement.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    //[Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICreateCustomerHandler _createCustomerHandler;
        private readonly ICurrentRequest _currentRequest;


        public CustomerController(
            ICreateCustomerHandler createCustomerHandler,
            ICurrentRequest currentRequest)
        {
            _createCustomerHandler = createCustomerHandler;
            _currentRequest = currentRequest;
        }
        [HttpGet("GetId")]
        public async Task<object> GetByIdAsync()
        {
            return await Task.FromResult<object>(null); ;
        }
        [HttpGet("GetId2")]
        public object GetByIdw()
        {
            return null;
        }
        //[HttpGet]
        //public string Get()
        //{
        //    return null;
        //    var item = User;
        //    var dd = _currentRequest;
        //    return "OK";
        //}

        [HttpPost]
        public async Task<Result> Post(CreateCustomer createCustomer)
        {
            return await _createCustomerHandler.Handler(createCustomer);
        }

    }
}
