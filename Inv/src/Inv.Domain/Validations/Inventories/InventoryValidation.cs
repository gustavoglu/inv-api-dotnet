using FluentValidation;
using Inv.Domain.Commands.Inventories;

namespace Inv.Domain.Validations.Inventories
{
    public abstract class InventoryValidation<T> : AbstractValidator<T> where T : InventoryCommand
    {
        protected void NameValidation()
        {
            RuleFor(inventory => inventory.Name).NotEmpty().NotNull().MaximumLength(100);
        }

        protected void UserIdValidation()
        {
            RuleFor(inventory => inventory.UserId).NotEmpty().NotNull();
        }

        protected void InputsValidation()
        {
            When(inventory => inventory.Inputs != null && inventory.Inputs.Any(),
               () => RuleForEach(inventory => inventory.Inputs).SetValidator(new InventoryFormInputValidation()));
        }

    
    }
}
