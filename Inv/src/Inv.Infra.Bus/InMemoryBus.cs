using Inv.Domain.Core.Bus;
using Inv.Domain.Core.Commands;
using Inv.Domain.Core.Events;
using MediatR;

namespace Inv.Infra.Bus
{
    public class InMemoryBus : IBus
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            _mediator.Publish(@event);
            return Task.CompletedTask;
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            _mediator.Send(command);
            return Task.CompletedTask;
        }
    }
}