using CustomerManagement.Domain.CustomerManagmentDbContext;
using CustomerManagement.Domain.Models.CustomerAggregate;
using CustomerManagement.Domain.Models.CustomerAggregate.Repository;
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
