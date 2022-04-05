using FluentValidation.Results;
using Inv.Domain.Core.Events;

namespace Inv.Domain.Core.Commands
{
    public abstract class Command : Message
    {
        private ValidationResult ValidationResult;
        public Command()
        {
            ValidationResult = new ValidationResult();
        }

        public void SetValidationResult(ValidationResult validationResult) => ValidationResult = validationResult;

        public List<ValidationFailure> GetValidationResultErrors() => ValidationResult.Errors;

        public virtual bool IsValid() => ValidationResult.IsValid;

    }
}
