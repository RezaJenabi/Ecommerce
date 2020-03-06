using Domain.Models.CustomerAggregate.Events.DomainEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Models.CustomerAggregate.Events.DomainEventHandlers
{
    public class CustomerCreateEventHandler : INotificationHandler<CustomerCreateEvent>
    {

        public CustomerCreateEventHandler()
        {
        }

        public Task Handle(CustomerCreateEvent notification, CancellationToken cancellationToken)
        {
            //send message
            return null;
        }
    }
}
