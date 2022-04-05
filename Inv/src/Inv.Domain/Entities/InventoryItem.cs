using Inv.Domain.Core.Entities;
using Inv.Domain.ObjectValues;

namespace Inv.Domain.Entities
{
    public class InventoryItem : Entity
    {
        public InventoryItem(DateTime date, Guid userId, Guid inventoryId, List<InventoryItemInput> inputs)
        {
            Date = date;
            UserId = userId;
            InventoryId = inventoryId;
            Inputs = inputs ?? new();
        }

        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public Guid InventoryId { get; set; }
        public List<InventoryItemInput> Inputs { get; set; }
    }
}
