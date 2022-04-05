using FluentValidation;
using Inv.Domain.Commands.InventoryItems;

namespace Inv.Domain.Validations.InventoryItems
{
    public class InventoryItemValidation<T> : AbstractValidator<T> where T : InventoryItemCommand
    {
        protected void DateValidation()
        {
            RuleFor(inventoryItem => inventoryItem.Date).NotNull();
        }

        protected void UserIdValidation()
        {
            RuleFor(inventoryItem => inventoryItem.UserId).NotNull();
        }

        protected void InventoryIdValidation()
        {
            RuleFor(inventoryItem => inventoryItem.InventoryId).NotNull();
        }

        protected void InputsValidation()
        {
            When(inventoryItem => inventoryItem.Inputs != null && inventoryItem.Inputs.Any(), () =>
            {
                RuleForEach(inventoryItem => inventoryItem.Inputs).SetValidator(new InventoryItemInputValidation());
            });
          
        }
    }
}
