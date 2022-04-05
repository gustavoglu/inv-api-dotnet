using Inv.Domain.Core.Entities;
using Inv.Domain.ObjectValues;

namespace Inv.Domain.Entities
{
    public class Inventory : Entity
    {
        public Inventory(string name, Guid userId, List<Guid> userIds = null, List<InventoryFormInput> inputs = null)
        {
            Name = name;
            UserId = userId;
            UserIds = userIds ?? new();
            Inputs = inputs ?? new();
        }

        public string Name { get; set; }
        public Guid UserId { get; set; }
        public List<InventoryFormInput> Inputs { get; set; }
        public List<Guid> UserIds { get; set; }
    }
}
