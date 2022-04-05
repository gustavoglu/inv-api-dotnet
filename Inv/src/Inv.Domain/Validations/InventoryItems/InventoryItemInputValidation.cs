using FluentValidation;
using Inv.Domain.ObjectValues;

namespace Inv.Domain.Validations.InventoryItems
{
    public class InventoryItemInputValidation : AbstractValidator<InventoryItemInput>
    {
        public InventoryItemInputValidation()
        {
            InputNameValidation();
            ValueValidation();
        }
        protected void InputNameValidation()
        {
            RuleFor(input => input.InputName).NotNull().NotEmpty().MaximumLength(100);
        }

        protected void ValueValidation()
        {
            RuleFor(input => input.Value).NotNull().NotEmpty().MaximumLength(250);
        }
    }
}
