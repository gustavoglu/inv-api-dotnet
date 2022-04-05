using FluentValidation;
using Inv.Domain.ObjectValues;

namespace Inv.Domain.Validations.Inventories
{
    public class InventoryFormInputValidation : AbstractValidator<InventoryFormInput>
    {
        public InventoryFormInputValidation()
        {
            NameValidation();
            TypeValidation();
        }
        protected void NameValidation()
        {
            RuleFor(input => input.Name).NotNull().NotEmpty().MaximumLength(50);
        }

        protected void TypeValidation()
        {
            RuleFor(input => input.Type).NotNull();
        }
    }
}

