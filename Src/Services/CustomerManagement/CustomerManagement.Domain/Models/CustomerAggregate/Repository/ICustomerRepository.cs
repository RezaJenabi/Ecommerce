using Infrastructure.Core.Repositories;

namespace CustomerManagement.Domain.Models.CustomerAggregate.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
    }
}
