using Inv.Domain.Validations.InventoryItems;

namespace Inv.Domain.Commands.InventoryItems
{
    public class InventoryItemInsertCommand : InventoryItemCommand
    {
        public override bool IsValid()
        {
            SetValidationResult(new InventoryItemInsertValidation().Validate(this));
            return base.IsValid();
        }
    }
}
