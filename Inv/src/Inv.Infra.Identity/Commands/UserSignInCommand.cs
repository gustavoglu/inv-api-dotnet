using Inv.Domain.Core.Commands;
using Inv.Infra.Identity.Validations;

namespace Inv.Infra.Identity.Commands
{
    public class UserSignInCommand : Command
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public override bool IsValid()
        {

            SetValidationResult(new UserSignInValidation().Validate(this));
            return base.IsValid();
        }
    }
}
