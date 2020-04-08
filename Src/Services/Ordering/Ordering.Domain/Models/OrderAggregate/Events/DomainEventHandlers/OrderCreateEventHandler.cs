using MediatR;
using Ordering.Domain.Models.OrderAggregate.Events.DomainEvents;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Domain.Models.OrderAggregate.Events.DomainEventHandlers
{
    public class OrderCreateEventHandler : INotificationHandler<OrderCreateEvent>
    {

        public OrderCreateEventHandler()
        {
        }

        public async Task Handle(OrderCreateEvent notification, CancellationToken cancellationToken)
        {
            //throw new System.NotImplementedException();
        }
    }
}
