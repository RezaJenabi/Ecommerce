using Infrastructure.Core.Repositories;
using Infrastructure.Domain.Repositories;
using Ordering.Domain.Models.OrderAggregate;

namespace Ordering.Domain.Models.OrderAggregate.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
    }
}
