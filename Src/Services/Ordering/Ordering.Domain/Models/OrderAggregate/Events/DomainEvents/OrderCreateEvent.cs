using MediatR;

namespace Ordering.Domain.Models.OrderAggregate.Events.DomainEvents
{
    public class OrderCreateEvent : INotification
    {
        public Order Order { get; set; }

        public OrderCreateEvent(Order Order)
        {
            Order = Order;
        }
    }
}
