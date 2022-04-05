using Inv.Domain.Core.Bus;
using Inv.Domain.Core.Notifications;
using Inv.Infra.Identity.Commands;
using Inv.Infra.Identity.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Inv.Services.Api.Controllers
{
    public class UserController : ControllerBaseApp
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserController(IBus bus, IDomainNotificationService notifications, IUserRepository userRepository, IConfiguration configuration) : base(bus, notifications)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterCommand command)
        {
            Bus.SendCommand(command);
            return ResponseDefault();
        }

        [HttpPost("/api/token")]
        public async Task<IActionResult> SignIn([FromBody] UserSignInCommand command)
        {
            await Bus.SendCommand(command);

            if (Notifications.HasNotification())
                return ResponseDefault();

            var user = _userRepository.GetByEmail(command.Email);

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                new Claim("userid", user.Id.ToString())
            };


            claims.AddRange(user.Claims.Select(claim => claim.ToClaim()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
            var now = DateTime.Now;
            var expireted = now.AddDays(1);

            var handler = new JwtSecurityTokenHandler();

            var descriptor = new SecurityTokenDescriptor()
            {
                Expires = expireted,
                NotBefore = now,
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = handler.CreateToken(descriptor);
            var tokenWrite = handler.WriteToken(token);

            var responseToken = new
            {
                Email = user.Email,
                Name = user.Name,
                Expires = expireted,
                TokenAccess = tokenWrite,
                Claims = claims

            };

            return ResponseDefault(responseToken);
        }
    }
}
