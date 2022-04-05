using Inv.Domain.CommandHandler;
using Inv.Domain.Commands;
using Inv.Domain.Commands.Inventories;
using Inv.Domain.Commands.InventoryItems;
using Inv.Domain.Core.Bus;
using Inv.Domain.Core.Notifications;
using Inv.Domain.Interfaces.Repositories;
using Inv.Domain.Interfaces.Users;
using Inv.Domain.Mappers;
using Inv.Infra.Bus;
using Inv.Infra.Data.Context;
using Inv.Infra.Data.Repositories;
using Inv.Infra.Identity.CommandHandlers;
using Inv.Infra.Identity.Commands;
using Inv.Infra.Identity.Interfaces;
using Inv.Infra.Identity.Repositories;
using Inv.Infra.Identity.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Inv.Infra.CrossCutting.IoC
{
    public class NativeInjection
    {
        public static void InjectDependencies(IServiceCollection services)
        {
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(typeof(CommandToEntityProfile));
      
            // domain
            services.AddScoped<IBus, InMemoryBus>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<IDomainNotificationService,DomainNotificationService>();

            services.AddScoped<IRequestHandler<InventoryInsertCommand, bool>, InventoryCommandHandler>();
            services.AddScoped<IRequestHandler<InventoryUpdateCommand, bool>, InventoryCommandHandler>();
            services.AddScoped<IRequestHandler<EntityDeleteCommand<InventoryCommand>, bool>, InventoryCommandHandler>();

            services.AddScoped<IRequestHandler<InventoryItemInsertCommand, bool>, InventoryItemCommandHandler>();
            services.AddScoped<IRequestHandler<InventoryItemUpdateCommand, bool>, InventoryItemCommandHandler>();
            services.AddScoped<IRequestHandler<EntityDeleteCommand<InventoryItemCommand>, bool>, InventoryItemCommandHandler>();

            // data
            services.AddScoped<MongoDbContext>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();


            // identity
            services.AddScoped<IRequestHandler<UserSignInCommand, bool>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<UserRegisterCommand, bool>, UserCommandHandler>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserAuthHelper, UserAuthHelper>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}