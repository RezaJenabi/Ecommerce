using CustomerManagement.Domain.CustomerManagmentContext;
using Domain.Models.CustomerAggregate;
using Domain.Models.CustomerAggregate.Repository;
using Infrastructure.Domain.Repositories;
using System.Collections.Generic;
using System.Text;

namespace CustomerManagement.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {

        public CustomerRepository(CustomerManagementDbContext customerManagementDbContext) 
            : base(customerManagementDbContext)
        {
        }

        public List<Customer> GetAllCustomers()
        {
           
            return null;
        }
    }
}
