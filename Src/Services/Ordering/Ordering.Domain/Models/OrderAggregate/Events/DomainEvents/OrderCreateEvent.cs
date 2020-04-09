using MediatR;

namespace Ordering.Domain.Models.OrderAggregate.Events.DomainEvents
{
    public class OrderCreateEvent : INotification
    {
        public Order _order { get; set; }

        public OrderCreateEvent(Order Order)
        {
            _order = Order;
        }
    }
}
