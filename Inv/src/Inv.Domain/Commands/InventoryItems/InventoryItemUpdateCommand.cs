using Inv.Domain.Validations.InventoryItems;

namespace Inv.Domain.Commands.InventoryItems
{
    public class InventoryItemUpdateCommand : InventoryItemCommand
    {
        public Guid Id { get; set; }

        public override bool IsValid()
        {
            SetValidationResult(new InventoryItemUpdateValidation().Validate(this));
            return base.IsValid();
        }
    }
}
