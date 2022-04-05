using FluentValidation;
using Inv.Domain.Commands;
using Inv.Domain.Core.Commands;

namespace Inv.Domain.Validations
{
    public class EntityDeleteValidation<T> : AbstractValidator<EntityDeleteCommand<T>> where T : Command
    {
        public EntityDeleteValidation()
        {
            RuleFor(entity => entity.Id).NotEmpty().NotEmpty();
        }
    }
}
