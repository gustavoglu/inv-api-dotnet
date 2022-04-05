using Inv.Domain.Commands.InventoryItems;

namespace Inv.Domain.Validations.InventoryItems
{
    public class InventoryItemInsertValidation : InventoryItemValidation<InventoryItemInsertCommand>
    {
        public InventoryItemInsertValidation()
        {
            DateValidation();
            UserIdValidation();
            InventoryIdValidation();
            InputsValidation();
        }
    }
}
