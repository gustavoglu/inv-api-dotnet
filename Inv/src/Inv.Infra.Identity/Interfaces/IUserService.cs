using Inv.Infra.Identity.Entities;

namespace Inv.Infra.Identity.Interfaces
{
    public interface IUserService
    {
        void CreateAdminUserIfNotExists();
        bool Login(string email, string password);
        void Register(User user, string password);
        void ResetPassword(User user, string newPassword);
        void UpdateEmail(User user, string newEmail);
        void UpdatePassword(User user, string oldPassword, string newPassword);
    }
}