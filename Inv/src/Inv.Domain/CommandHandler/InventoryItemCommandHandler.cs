using AutoMapper;
using Inv.Domain.Commands;
using Inv.Domain.Commands.Inventories;
using Inv.Domain.Commands.InventoryItems;
using Inv.Domain.Core.Notifications;
using Inv.Domain.Entities;
using Inv.Domain.Interfaces.Repositories;
using Inv.Domain.Interfaces.Users;
using MediatR;

namespace Inv.Domain.CommandHandler
{
    public class InventoryItemCommandHandler : CommandHandler, IRequestHandler<InventoryItemInsertCommand, bool>,
        IRequestHandler<InventoryItemUpdateCommand, bool>,
         IRequestHandler<EntityDeleteCommand<InventoryItemCommand>, bool>

    {
        private readonly IInventoryItemRepository _repository;
        private readonly IUserAuthHelper _userAuthHelper;

        public InventoryItemCommandHandler(IMapper mapper, IDomainNotificationService notifications, IInventoryItemRepository repository, IUserAuthHelper userAuthHelper) : base(mapper, notifications)
        {
            _repository = repository;
            _userAuthHelper = userAuthHelper;
        }

        public Task<bool> Handle(InventoryItemInsertCommand request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return Task.FromResult(false);

            var entity = Mapper.Map<InventoryItem>(request);
            entity.UserId = _userAuthHelper.GetUserId().Value;
            _repository.Insert(entity);

            return Task.FromResult(true);
        }

        public Task<bool> Handle(InventoryItemUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return Task.FromResult(false);

            var entity = Mapper.Map<InventoryItem>(request);
            _repository.Update(entity);

            return Task.FromResult(true);
        }

        public Task<bool> Handle(EntityDeleteCommand<InventoryItemCommand> request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return Task.FromResult(false);
            _repository.Delete(request.Id);
            return Task.FromResult(true);
        }
    }
}
