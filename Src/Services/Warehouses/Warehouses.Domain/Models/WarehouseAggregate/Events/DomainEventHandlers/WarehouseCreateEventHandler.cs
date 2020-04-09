using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Warehouses.Domain.Models.WarehouseAggregate.Events.DomainEvents;

namespace Warehouses.Domain.Models.WarehouseAggregate.Events.DomainEventHandlers
{
    public class WarehouseCreateEventHandler : INotificationHandler<WarehouseCreateEvent>
    {

        public WarehouseCreateEventHandler()
        {
        }

        public async Task Handle(WarehouseCreateEvent notification, CancellationToken cancellationToken)
        {
            //throw new System.NotImplementedException();
        }
    }
}
