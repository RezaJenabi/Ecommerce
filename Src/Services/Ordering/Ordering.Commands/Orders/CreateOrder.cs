using FluentValidation;
using Infrastructure.Utilities.Common;
using Ordering.Commands.Resources;

namespace Ordering.Commands.Orders
{
    public class CreateOrder : Request<Result>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
    public class CreateOrderValidator : AbstractValidator<CreateOrder>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.FirstName).NotNull().WithMessage(Validation.FirstNameRequired);
            RuleFor(x => x.LastName).NotNull().WithMessage(Validation.LastNameRequired);
        }
    }
}
