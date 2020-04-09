using FluentValidation;
using Infrastructure.Utilities.Common;
using Warehouses.Commands.Resources;

namespace Warehouses.Commands.Warehouses
{
    public class CreateWarehouse : Request<Result>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
    public class CreateWarehouseValidator : AbstractValidator<CreateWarehouse>
    {
        public CreateWarehouseValidator()
        {
            RuleFor(x => x.FirstName).NotNull().WithMessage(Validation.FirstNameRequired);
            RuleFor(x => x.LastName).NotNull().WithMessage(Validation.LastNameRequired);
        }
    }
}
