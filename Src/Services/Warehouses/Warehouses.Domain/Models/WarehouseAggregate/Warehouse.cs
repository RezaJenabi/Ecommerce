using Infrastructure.Core.BaseEntities;
using Warehouses.Domain.Models.WarehouseAggregate.Events.DomainEvents;

namespace Warehouses.Domain.Models.WarehouseAggregate
{

    public class Warehouse : BaseEntity, IAggregateRoot
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public bool IsActive { get; private set; }

        private Warehouse()
        {

        }

        public Warehouse(string fistName, string lastName, string email, bool isActive)
        {
            FirstName = fistName;
            LastName = lastName;
            Email = email;
            IsActive = isActive;
            this.AddDomainEvent(new WarehouseCreateEvent(this));
        }

        public Warehouse(long id, string fistName, string lastName, string email, bool isActive)
        {
            Id = id;
            FirstName = fistName;
            LastName = lastName;
            Email = email;
            IsActive = isActive;
        }

        public void ChangeEmail(string email)
        {
            Email = email;
        }

        public bool Validation()
        {
            return true;
        }
    }
}