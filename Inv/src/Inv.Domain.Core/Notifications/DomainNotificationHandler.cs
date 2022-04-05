using MediatR;

namespace Inv.Domain.Core.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        public List<DomainNotification> Notifications { get; set; }

        public DomainNotificationHandler()
        {
            Notifications = new();
        }
        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            Notifications.Add(notification);
            return Task.CompletedTask;
        }

        public void Dispose() => Notifications.Clear();
    }
}
