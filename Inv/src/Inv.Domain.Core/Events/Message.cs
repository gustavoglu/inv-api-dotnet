using MediatR;

namespace Inv.Domain.Core.Events
{
    public class Message : IRequest<bool>
    {
        public Message(string messageType = null, Guid? aggregateId = null)
        {
            MessageType = messageType ?? this.GetType().Name;
            AggregateId = aggregateId;
        }

        private string MessageType { get; set; }
        private Guid? AggregateId { get; set; }
    }
}
