using GREEDY.Models;
using System.Collections.Generic;

namespace GREEDY.DataManagers
{
    public interface IUserManager
    {
        void RegisterNewUser(RegistrationCredentials credentials);
        List<User> GetExistingUsers();
        void AddUser(User user);
    }
}
