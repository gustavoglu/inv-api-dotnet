using Inv.Domain.Interfaces.Repositories;
using Inv.Infra.Identity.Entities;

namespace Inv.Infra.Identity.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);
        void UpdateEmail(Guid id, string newEmail);
        void UpdatePasswordHash(Guid id, string newHash);
    }
}