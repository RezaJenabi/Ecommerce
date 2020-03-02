using FluentValidation;
using Infrastructure.Common;
using CustomerManagement.Commands.Resources;

namespace Commands.Customers
{
    public class CreateCustomer : Request<Result>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
    public class CreateCustomerValidator : AbstractValidator<CreateCustomer>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.FirstName).NotNull().WithMessage(Validaton.FirstNameRequired);
            RuleFor(x => x.LastName).NotNull().WithMessage(Validaton.LastNameRequired);
        }
    }
}
