using System;
using System.Threading.Tasks;
using CustomerManagement.Commands.Customers;
using CustomerManagement.Domain.Models.CustomerAggregate;
using CustomerManagement.Domain.Models.CustomerAggregate.Repository;
using Infrastructure.Utilities.Commands;
using Infrastructure.Utilities.Common;

namespace CustomerManagement.CommandsHandler.Customers
{
    public class CreateCustomerHandler : MessageHandler<CreateCustomer, Result>, ICreateCustomerHandler
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public override async Task<Result> Handler(CreateCustomer message)
        {
            var customer = new Customer(message.FirstName, message.LastName, message.Email, message.IsActive);
            await _customerRepository.AddAsync(customer);
            await _customerRepository.SaveEntitiesAsync();

            return new Result();
        }
    }

    public interface ICreateCustomerHandler
    {
        Task<Result> Handler(CreateCustomer message);
    }

}
