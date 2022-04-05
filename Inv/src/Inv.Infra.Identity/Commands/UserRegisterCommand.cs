using Inv.Domain.Core.Commands;
using Inv.Infra.Identity.Validations;

namespace Inv.Infra.Identity.Commands
{
    public class UserRegisterCommand : Command
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public override bool IsValid()
        {
            SetValidationResult(new UserRegisterValidation().Validate(this));
            return base.IsValid();
        }
    }
}
