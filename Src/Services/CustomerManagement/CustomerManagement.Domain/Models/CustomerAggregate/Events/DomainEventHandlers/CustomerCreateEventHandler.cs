using CustomerManagement.Domain.Models.CustomerAggregate.Events.DomainEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerManagement.Domain.Models.CustomerAggregate.Events.DomainEventHandlers
{
    public class CustomerCreateEventHandler : INotificationHandler<CustomerCreateEvent>
    {

        public CustomerCreateEventHandler()
        {
        }

        public async Task Handle(CustomerCreateEvent notification, CancellationToken cancellationToken)
        {
            //throw new System.NotImplementedException();
        }
    }
}
