using AutoMapper;
using Inv.Domain.CommandHandler;
using Inv.Domain.Core.Notifications;
using Inv.Infra.Identity.Commands;
using Inv.Infra.Identity.Entities;
using Inv.Infra.Identity.Interfaces;
using MediatR;

namespace Inv.Infra.Identity.CommandHandlers
{
    public class UserCommandHandler : CommandHandler, IRequestHandler<UserSignInCommand, bool>, IRequestHandler<UserRegisterCommand, bool>
    {
        private readonly IUserService _userService;
        public UserCommandHandler(IMapper mapper, IDomainNotificationService notifications, IUserService userService) : base(mapper, notifications)
        {
            _userService = userService;
        }

        public Task<bool> Handle(UserSignInCommand request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return Task.FromResult(false);

            var signInRes = _userService.Login(request.Email, request.Password);
            if (!signInRes)
            {
                Notifications.Add("UserService", "Email or Password incorrect");
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return Task.FromResult(false);

            _userService.Register(new User(request.Name, request.Email, request.Email, null, null), request.Password);

            return Task.FromResult(!Notifications.HasNotification());

        }
    }
}
