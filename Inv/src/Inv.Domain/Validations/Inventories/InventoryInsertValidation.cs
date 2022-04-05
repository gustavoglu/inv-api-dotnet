using Inv.Domain.Commands.Inventories;

namespace Inv.Domain.Validations.Inventories
{
    public class InventoryInsertValidation : InventoryValidation<InventoryInsertCommand>
    {
        public InventoryInsertValidation()
        {
            NameValidation();
            InputsValidation();
            UserIdValidation();
        }
    }
}
