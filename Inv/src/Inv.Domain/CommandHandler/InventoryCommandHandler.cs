using AutoMapper;
using Inv.Domain.Commands;
using Inv.Domain.Commands.Inventories;
using Inv.Domain.Core.Notifications;
using Inv.Domain.Entities;
using Inv.Domain.Interfaces.Repositories;
using Inv.Domain.Interfaces.Users;
using MediatR;

namespace Inv.Domain.CommandHandler
{
    public class InventoryCommandHandler : CommandHandler, IRequestHandler<InventoryInsertCommand,bool>, 
        IRequestHandler<InventoryUpdateCommand, bool>,
         IRequestHandler<EntityDeleteCommand<InventoryCommand>,bool>

    {
        private readonly IInventoryRepository _repository;
        private readonly IUserAuthHelper _userAuthHelper;

        public InventoryCommandHandler(IMapper mapper, IDomainNotificationService notifications, IInventoryRepository repository, IUserAuthHelper userAuthHelper) : base(mapper, notifications)
        {
            _repository = repository;
            _userAuthHelper = userAuthHelper;
        }

        public Task<bool> Handle(InventoryInsertCommand request, CancellationToken cancellationToken)
        {
            if(!CommandIsValid(request)) return Task.FromResult(false);

            var entity = Mapper.Map<Inventory>(request);
            entity.UserId = _userAuthHelper.GetUserId().Value;
            _repository.Insert(entity);

            return Task.FromResult(true);
        }

        public Task<bool> Handle(InventoryUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return Task.FromResult(false);

            var entity = Mapper.Map<Inventory>(request);
            _repository.Update(entity);

            return Task.FromResult(true);
        }

        public Task<bool> Handle(EntityDeleteCommand<InventoryCommand> request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return Task.FromResult(false);
            _repository.Delete(request.Id);
            return Task.FromResult(true);
        }
    }
}
