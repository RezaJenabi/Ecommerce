using Infrastructure.Core.BaseEntities;
using Ordering.Domain.Models.OrderAggregate.Events.DomainEvents;

namespace Ordering.Domain.Models.OrderAggregate
{

    public class Order : BaseEntity, IAggregateRoot
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public bool IsActive { get; private set; }

        private Order()
        {

        }

        public Order(string fistName, string lastName, string email, bool isActive)
        {
            FirstName = fistName;
            LastName = lastName;
            Email = email;
            IsActive = isActive;
            this.AddDomainEvent(new OrderCreateEvent(this));
        }

        public Order(long id, string fistName, string lastName, string email, bool isActive)
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