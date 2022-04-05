using AutoMapper;
using Inv.Domain.Core.Commands;
using Inv.Domain.Core.Notifications;

namespace Inv.Domain.CommandHandler
{
    public abstract class CommandHandler
    {
        protected IMapper Mapper;
        protected IDomainNotificationService Notifications;

        protected CommandHandler(IMapper mapper, IDomainNotificationService notifications)
        {
            Mapper = mapper;
            Notifications = notifications;
        }

        protected bool CommandIsValid<T>(T command) where T : Command
        {
            if (command.IsValid()) return true;

            var errors = command.GetValidationResultErrors().Select(error => new DomainNotification("Validation", error.ErrorMessage));
            Notifications.AddRange(errors);
            return false;
        }
    }
}
