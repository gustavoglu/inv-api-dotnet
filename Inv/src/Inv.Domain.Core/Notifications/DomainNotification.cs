using Inv.Domain.Core.Events;

namespace Inv.Domain.Core.Notifications
{
    public class DomainNotification : Event
    {
        public DomainNotification(string type, string value)
        {
            Version = 1;
            Id = Guid.NewGuid();
            Type = type;
            Value = value;
        }

        public Guid Id { get; set; }
        public int Version { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
