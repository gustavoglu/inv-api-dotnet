using FluentValidation;
using Inv.Domain.Commands.Inventories;

namespace Inv.Domain.Validations.Inventories
{
    public class InventoryUpdateValidation : InventoryValidation<InventoryUpdateCommand>
    {
        public InventoryUpdateValidation()
        {
            IdValidation();
            NameValidation();
            InputsValidation();
            UserIdValidation();
        }

        public void IdValidation()
        {
            RuleFor(inventory => inventory.Id).NotEmpty().NotNull();
        }

    }
}
