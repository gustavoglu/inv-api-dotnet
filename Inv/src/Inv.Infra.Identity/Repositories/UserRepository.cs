using Inv.Infra.Data.Context;
using Inv.Infra.Data.Repositories;
using Inv.Infra.Identity.Entities;
using Inv.Infra.Identity.Interfaces;
using MongoDB.Driver;

namespace Inv.Infra.Identity.Repositories
{
    public class UserRepository : Repository<Entities.User>, IUserRepository
    {
        public UserRepository(MongoDbContext context) : base(context)
        {
        }


        public User GetByEmail(string email)
        {
            var userNameFilter = Builders<User>.Filter.Eq(u => u.Email, email);

            return Collection.Find(userNameFilter).FirstOrDefault();
        }

        public void UpdateEmail(Guid id, string newEmail)
        {
            var userIdFilter = Builders<User>.Filter.Eq(u => u.Id, id);
            var emailUpdate = Builders<User>.Update.Set(u => u.Email, newEmail);
            Collection.UpdateOne(userIdFilter, emailUpdate);
        }

        public void UpdatePasswordHash(Guid id, string newHash)
        {
            var userIdFilter = Builders<User>.Filter.Eq(u => u.Id, id);
            var hashUserUpdate = Builders<User>.Update.Set(u => u.Hash, newHash);
            Collection.UpdateOne(userIdFilter, hashUserUpdate);
        }

    }
}
