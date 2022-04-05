using Inv.Domain.Validations.Inventories;

namespace Inv.Domain.Commands.Inventories
{
    public class InventoryUpdateCommand : InventoryCommand
    {
        public Guid Id { get; set; }

        public override bool IsValid()
        {
            SetValidationResult(new InventoryUpdateValidation().Validate(this));
            return base.IsValid();
        }
    }
}
