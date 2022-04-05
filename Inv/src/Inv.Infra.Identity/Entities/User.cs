using Inv.Domain.Core.Entities;
using Inv.Infra.Identity.ObjectValues;

namespace Inv.Infra.Identity.Entities
{
    public class User : Entity
    {
        public User(string name, string username, string email, string hash, List<UserClaim> claims = null)
        {
            Name = name;
            Username = username;
            Email = email;
            Hash = hash;
            Claims = claims ?? new();
        }

        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public List<UserClaim> Claims { get; set; }
    }
}
