using FluentValidation;
using Inv.Domain.Commands.InventoryItems;

namespace Inv.Domain.Validations.InventoryItems
{
    public class InventoryItemUpdateValidation : InventoryItemValidation<InventoryItemUpdateCommand>
    {
        public InventoryItemUpdateValidation()
        {
            DateValidation();
            UserIdValidation();
            InventoryIdValidation();
            InputsValidation();
        }

        protected void IdValidation()
        {
            RuleFor(inventoryItem => inventoryItem.Id).NotNull();
        }
    }
   
}
