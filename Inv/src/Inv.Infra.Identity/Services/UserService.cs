using Inv.Domain.Core.Bus;
using Inv.Domain.Core.Notifications;
using Inv.Infra.Identity.Entities;
using Inv.Infra.Identity.Interfaces;
using Inv.Infra.Identity.ObjectValues;
using System.Security.Cryptography;

namespace Inv.Infra.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;
        public UserService(IUserRepository userRepository, IBus bus)
        {
            _userRepository = userRepository;
            _bus = bus;
        }

        private static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        private bool ByteArraysEqual(byte[] b1, byte[] b2)
        {
            if (b1 == b2) return true;
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i]) return false;
            }
            return true;
        }

        private bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }

        public void Register(User user, string password)
        {
            var userExists = _userRepository.Exists(u => u.Email == user.Email);

            if (userExists)
            {
                _bus.RaiseEvent(new DomainNotification("UserService", "username already exists"));
                return;
            }

            string hash = HashPassword(password);
            user.Hash = hash;

            _userRepository.Insert(user);
        }


        public void UpdateEmail(User user, string newEmail)
        {
            var exists = _userRepository.GetByEmail(newEmail) != null;

            if (!exists)
            {
                _bus.RaiseEvent(new DomainNotification("UserService", "Username already exists"));
                return;
            }

            _userRepository.UpdateEmail(user.Id, newEmail);
        }

        public void UpdatePassword(User user, string oldPassword, string newPassword)
        {
            bool valid = VerifyHashedPassword(user.Hash, oldPassword);
            if (!valid)
            {
                _bus.RaiseEvent(new DomainNotification("UserService", "Password Incorrect"));
                return;
            }

            ResetPassword(user, newPassword);
        }

        public void ResetPassword(User user, string newPassword)
        {
            _userRepository.UpdatePasswordHash(user.Id, HashPassword(newPassword));
        }

        public void CreateAdminUserIfNotExists()
        {
            string adminEmail = "admin@admin.com";
            bool userExist = _userRepository.Exists(user => user.Email == adminEmail);
            if (userExist) return;

            var masterUserClaim = new UserClaim("master", "master");
            var hash = HashPassword("Admin123*");
            User user = new User(null, adminEmail, adminEmail, hash);
            user.Claims.Add(masterUserClaim);
            _userRepository.Insert(user);

        }

        public bool Login(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);

            if (user is null)
            {
                _bus.RaiseEvent(new DomainNotification("UserService", "User not found"));
                return false;
            }

            var hashOk = VerifyHashedPassword(user.Hash, password);
            if (!hashOk)
            {
                _bus.RaiseEvent(new DomainNotification("UserService", "Username or password incorrect"));
                return false;
            }

            return true;
        }

    }
}
