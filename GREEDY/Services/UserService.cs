using GREEDY.DataManagers;
using GREEDY.Extensions;
using GREEDY.Models;

namespace GREEDY.Services
{
    public class UserService : IUserService
    {
        private readonly IUserManager _userManager;
        public UserService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public User LoginByUsernameOrEmail(LoginCredentials credentials)
        {
            var user = _userManager.FindByUsername(credentials.Username, false);
            if (user == null)
            {
                user = _userManager.FindByEmail(credentials.Username, false);
            }
            if (user == null)
            {
                return null;
            }
            if (VerifyPassword(user.Password, credentials.Password))
            {
                return user;
            }
            return null;
        }

        private bool VerifyPassword(string userPassword, string credentialsPassword)
        {
            return userPassword.Decrypt() == credentialsPassword;
        }
    }
}
