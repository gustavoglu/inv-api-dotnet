using FluentValidation;
using Inv.Infra.Identity.Commands;

namespace Inv.Infra.Identity.Validations
{
    public class UserRegisterValidation : AbstractValidator<UserRegisterCommand>
    {
        public UserRegisterValidation()
        {
            NameValidation();
            EmailValidation();
            PasswordValidation(); 
            ConfirmPasswordValidation();
        }
        protected void NameValidation()
        {
            RuleFor(user => user.Name).NotEmpty().NotNull();
        }

        protected void EmailValidation()
        {
            RuleFor(user => user.Email).EmailAddress().NotEmpty().NotNull();
        }

        protected void PasswordValidation()
        {
            RuleFor(user => user.Password).MinimumLength(6).NotEmpty().NotNull();
        }

        protected void ConfirmPasswordValidation()
        {
            RuleFor(user => user.ConfirmPassword).Equal(user => user.Password).MinimumLength(6).NotEmpty().NotNull();
        }
    }
}
