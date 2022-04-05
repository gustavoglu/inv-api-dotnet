namespace Inv.Domain.Core.Notifications
{
    public interface  IDomainNotificationService
    {
        void Add(DomainNotification notification);
        void AddRange(IEnumerable<DomainNotification> notifications);
        bool HasNotification();
        List<DomainNotification> GetNotifications();
        void ClearNotifications();
        void Add(string type, string value);
    }
}
