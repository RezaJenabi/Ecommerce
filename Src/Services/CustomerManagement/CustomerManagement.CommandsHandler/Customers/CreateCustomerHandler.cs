using System;
using System.Threading.Tasks;
using Commands.Customers;
using CustomerManagement.Domain.DbContext;
using Domain.Models.CustomerAggregate;
using Infrastructure.Commands;
using Infrastructure.Common;
using Infrastructure.Domain.IRepository;
using MediatR;

namespace CommandsHandler.Customers
{
    public class CreateCustomerHandler : MessageHandler<CreateCustomer, Result>, ICreateCustomerHandler
    {
        private readonly CustomerManagementDbContext _customerManagmentDbcontext;

        public CreateCustomerHandler(CustomerManagementDbContext customerManagmentDbcontext)
        {
            _customerManagmentDbcontext = customerManagmentDbcontext;
        }

        public override async Task<Result> Handler(CreateCustomer message)
        {
            var customer = new Customer(message.FirstName, message.LastName, message.Email,message.IsActive);
            await _customerManagmentDbcontext.Set<Customer>().AddAsync(customer);
            await _customerManagmentDbcontext.SaveEntitiesAsync();

            return new Result();
        }
    }

    public interface ICreateCustomerHandler
    {
        Task<Result> Handler(CreateCustomer message);
    }

}
