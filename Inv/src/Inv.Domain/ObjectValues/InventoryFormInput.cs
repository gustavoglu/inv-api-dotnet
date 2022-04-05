using Inv.Domain.Enums;

namespace Inv.Domain.ObjectValues
{
    public class InventoryFormInput
    {
        public InventoryFormInput(string name, EInventoryFormInputType type = EInventoryFormInputType.Text)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; set; }
        public EInventoryFormInputType Type { get; set; }

    }
}
