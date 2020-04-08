using CustomerManagement.Domain.Models.CustomerAggregate;
using Infrastructure.Core.Repositories;
using Infrastructure.Domain.Repositories;

namespace CustomerManagement.Domain.Models.CustomerAggregate.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
    }
}
