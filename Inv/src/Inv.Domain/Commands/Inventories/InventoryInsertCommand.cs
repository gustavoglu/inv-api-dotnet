using Inv.Domain.Validations.Inventories;

namespace Inv.Domain.Commands.Inventories
{
    public class InventoryInsertCommand : InventoryCommand
    {
        public override bool IsValid()
        {
            SetValidationResult(new InventoryInsertValidation().Validate(this));
            return base.IsValid();
        }
    }
}
