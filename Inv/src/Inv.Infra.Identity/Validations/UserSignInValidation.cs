using FluentValidation;
using Inv.Infra.Identity.Commands;

namespace Inv.Infra.Identity.Validations
{
    public class UserSignInValidation : AbstractValidator<UserSignInCommand>
    {
        public UserSignInValidation()
        {
            EmailValidation();
            PasswordValidation();
        }
        protected void EmailValidation()
        {
            RuleFor(signIn => signIn.Email).EmailAddress().NotEmpty().NotNull();
        }

        protected void PasswordValidation()
        {
            RuleFor(signIn => signIn.Password).NotEmpty().NotNull();
        }
    }
}
