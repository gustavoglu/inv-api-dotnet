using Inv.Domain.Core.Commands;
using Inv.Domain.ObjectValues;

namespace Inv.Domain.Commands.InventoryItems
{
    public abstract class InventoryItemCommand : Command
    {
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public Guid InventoryId { get; set; }
        public List<InventoryItemInput> Inputs { get; set; }
    }
}
