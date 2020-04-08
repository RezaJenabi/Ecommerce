using CustomerManagement.Domain.Models.CustomerAggregate;
using MediatR;

namespace CustomerManagement.Domain.Models.CustomerAggregate.Events.DomainEvents
{
    public class CustomerCreateEvent : INotification
    {
        public Customer Customer { get; set; }

        public CustomerCreateEvent(Customer customer)
        {
            Customer = customer;
        }
    }
}
