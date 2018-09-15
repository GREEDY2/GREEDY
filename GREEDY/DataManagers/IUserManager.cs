using System.Collections.Generic;
using GREEDY.Models;

namespace GREEDY.DataManagers
{
    public interface IUserManager
    {
        void RegisterNewUser(RegistrationCredentials credentials);
        List<User> GetExistingUsers();
        void AddUser(User user);
        User FindByUsername(string username, bool isFacebookUser);
        User FindByEmail(string email, bool isFacebookUser);
        bool ChangeUserEmail(string username, string encryptedPassword, string newEmail);
        bool ChangeUserPassword(string username, string encryptedPassword, string newEncryptedPassword);
    }
}