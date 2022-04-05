using Inv.Domain.Core.Commands;
using Inv.Domain.ObjectValues;

namespace Inv.Domain.Commands.Inventories
{
    public abstract class InventoryCommand  : Command
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public List<InventoryFormInput> Inputs { get; set; }
        public List<Guid> UserIds { get; set; }
    }
}
