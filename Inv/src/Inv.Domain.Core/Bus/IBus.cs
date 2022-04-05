using Inv.Domain.Core.Commands;
using Inv.Domain.Core.Events;

namespace Inv.Domain.Core.Bus
{
    public interface IBus
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}