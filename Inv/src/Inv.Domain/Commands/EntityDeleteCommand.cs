using Inv.Domain.Core.Commands;
using Inv.Domain.Validations;

namespace Inv.Domain.Commands
{
    public class EntityDeleteCommand<T> : Command where T : Command
    {
        public EntityDeleteCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public override bool IsValid()
        {
            SetValidationResult(new EntityDeleteValidation<T>().Validate(this));
            return base.IsValid();
        }
    }
}
