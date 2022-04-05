using System.Security.Claims;

namespace Inv.Infra.Identity.ObjectValues
{
    public class UserClaim
    {
        public UserClaim(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public string Type { get; set; }
        public string Value { get; set; }

        public Claim ToClaim() => new Claim(Type, Value);
    }
}
