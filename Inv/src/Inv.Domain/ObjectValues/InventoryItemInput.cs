namespace Inv.Domain.ObjectValues
{
    public class InventoryItemInput
    {
        public InventoryItemInput(string inputName, string value)
        {
            InputName = inputName;
            Value = value;
        }

        public string InputName { get; set; }
        public string Value { get; set; }
    }
}
