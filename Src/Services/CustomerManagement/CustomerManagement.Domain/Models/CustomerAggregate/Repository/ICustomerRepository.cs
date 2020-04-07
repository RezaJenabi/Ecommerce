using Infrastructure.Core.Repositories;
using Infrastructure.Domain.Repositories;

namespace Domain.Models.CustomerAggregate.Repository
{
    public interface ICustomerRepository:IRepository<Customer>
    {
    }
}
