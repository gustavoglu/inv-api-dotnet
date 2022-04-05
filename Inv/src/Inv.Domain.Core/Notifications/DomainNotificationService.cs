using Inv.Domain.Core.Bus;
using MediatR;

namespace Inv.Domain.Core.Notifications
{
    public class DomainNotificationService : IDomainNotificationService
    {
        private readonly IBus _bus;
        private readonly DomainNotificationHandler _handler;

        public DomainNotificationService(IBus bus, INotificationHandler<DomainNotification> handler)
        {
            _bus = bus;
            _handler = (DomainNotificationHandler)handler;
        }


        public void Add(string type, string value)
        {
            _bus.RaiseEvent(new DomainNotification(type ,value));
        }
        public void Add(DomainNotification notification)
        {
            _bus.RaiseEvent(notification);
        }

        public void AddRange(IEnumerable<DomainNotification> notifications)
        {
            foreach (var notification in notifications)
            
                _bus.RaiseEvent(notification);
            
        }

        public void ClearNotifications()
        {
            _handler.Dispose();
        }

        public List<DomainNotification> GetNotifications()
        {
            return _handler.Notifications;
        }

        public bool HasNotification()
        {
            return _handler.Notifications.Any();
        }
    }
}
