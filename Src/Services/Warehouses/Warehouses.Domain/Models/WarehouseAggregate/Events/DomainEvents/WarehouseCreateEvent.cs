using MediatR;

namespace Warehouses.Domain.Models.WarehouseAggregate.Events.DomainEvents
{
    public class WarehouseCreateEvent : INotification
    {
        public Warehouse _warehouse { get; set; }

        public WarehouseCreateEvent(Warehouse warehouse)
        {
            _warehouse = warehouse;
        }
    }
}
