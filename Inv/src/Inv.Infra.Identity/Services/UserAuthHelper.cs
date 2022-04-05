using Inv.Domain.Interfaces.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Infra.Identity.Services
{
    public class UserAuthHelper : IUserAuthHelper
    {
        private readonly IHttpContextAccessor _accessor;

        public UserAuthHelper(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public List<Claim> GetClaims()
        {
            return _accessor.HttpContext.User?.Claims?.ToList();
        }

        public Guid? GetUserId()
        {
            var id = _accessor.HttpContext.User?
                        .Claims?
                        .FirstOrDefault(claim => claim.Type == "userid")?
                        .Value;

            if (string.IsNullOrEmpty(id)) return null;
            return Guid.Parse(id);

        }

        public string GetUsername()
        {
            return  _accessor.HttpContext.User?.Identity?.Name;
        }

        public bool HasClaim(string type, string value)
        {
            return _accessor.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == "userid") != null;
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
