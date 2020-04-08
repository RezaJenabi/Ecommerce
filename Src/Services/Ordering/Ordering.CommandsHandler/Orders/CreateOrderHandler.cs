using Infrastructure.Commands;
using Infrastructure.Utilities.Common;
using Ordering.Commands.Orders;
using Ordering.Domain.Models.OrderAggregate;
using Ordering.Domain.Models.OrderAggregate.Repository;
using System.Threading.Tasks;

namespace Ordering.CommandsHandler.Orders
{
    public class CreateOrderHandler : MessageHandler<CreateOrder, Result>, ICreateOrderHandler
    {
        private readonly IOrderRepository _OrderRepository;

        public CreateOrderHandler(IOrderRepository OrderRepository)
        {
            _OrderRepository = OrderRepository;
        }

        public override async Task<Result> Handler(CreateOrder message)
        {
            var order = new Order(message.FirstName, message.LastName, message.Email, message.IsActive);
            await _OrderRepository.AddAsync(order);
            await _OrderRepository.SaveEntitiesAsync();

            return new Result();
        }
    }

    public interface ICreateOrderHandler
    {
        Task<Result> Handler(CreateOrder message);
    }

}
