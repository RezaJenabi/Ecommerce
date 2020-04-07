using Domain.Models.CustomerAggregate.Events.DomainEvents;
using Infrastructure.Core.BaseEntities;

namespace Domain.Models.CustomerAggregate
{

    public class Customer : BaseEntity, IAggregateRoot
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public bool IsActive { get; private set; }

        private Customer()
        {

        }

        public Customer(string fistName, string lastName, string email,bool isActive)
        {
            FirstName = fistName;
            LastName = lastName;
            Email = email;
            IsActive = isActive;
            this.AddDomainEvent(new CustomerCreateEvent(this));
        }

        public Customer(long id, string fistName, string lastName, string email, bool isActive)
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