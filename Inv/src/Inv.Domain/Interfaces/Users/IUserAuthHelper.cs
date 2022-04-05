using System.Security.Claims;

namespace Inv.Domain.Interfaces.Users
{
    public interface IUserAuthHelper
    {
        bool IsAuthenticated();
        List<Claim> GetClaims();
        Guid? GetUserId();
        string GetUsername();
        bool HasClaim(string type, string value);
    }
}
