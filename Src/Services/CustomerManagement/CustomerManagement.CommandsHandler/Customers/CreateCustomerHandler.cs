using System;
using System.Threading.Tasks;
using Commands.Customers;
using Domain.Models.CustomerAggregate;
using Infrastructure.Commands;
using Infrastructure.Common;
using Infrastructure.Domain.IRepository;

namespace CommandsHandler.Customers
{
    public class CreateCustomerHandler : MessageHandler<CreateCustomer, Result>, ICreateCustomerHandler
    {
        private readonly IRepository<Customer> _customerRepository;
        public CreateCustomerHandler(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public override async Task<Result> Handler(CreateCustomer message)
        {
            try
            {
                var iitem = new Customer("erter","erert","jenabiReza@gmail.com");
                //var customerCreated = message.Adapt<CustomerCreated>();
                //var customer = CustomerCreated.Create(customerCreated);
                await _customerRepository.AddAsync(iitem);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return new Result { Text = "OK" };
        }
    }

    public interface ICreateCustomerHandler
    {
        Task<Result> Handler(CreateCustomer message);
    }

}
