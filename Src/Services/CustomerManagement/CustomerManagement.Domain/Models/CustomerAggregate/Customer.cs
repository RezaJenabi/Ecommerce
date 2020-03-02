using Domain.Models.CustomerAggregate.Events.DomainEvents;
using Infrastructure.Domain.BaseEntities;

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

        public Customer(string fistName, string lastName, string email)
        {
            FirstName = fistName;
            LastName = lastName;
            Email = email;
            //this.AddDomainEvent(new CustomerCreateEvent(this));

        }

    }
}