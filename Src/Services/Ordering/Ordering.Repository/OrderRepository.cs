using Infrastructure.Domain.Repositories;
using Ordering.Domain.Models.OrderAggregate;
using Ordering.Domain.Models.OrderAggregate.Repository;
using Ordering.Domain.OrderingDbContext;
using System.Collections.Generic;

namespace Ordering.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {

        public OrderRepository(OrderingDbContext OrderManagementDbContext)
            : base(OrderManagementDbContext)
        {
        }

        public List<Order> GetAllOrders()
        {

            return null;
        }
    }
}
