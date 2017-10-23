using GREEDY.Extensions;
using GREEDY.Models;
using System.Collections.Generic;
using GREEDY.Data;
using System.Linq;

namespace GREEDY.DataManagers
{
    public static class UserManager
    {
        //TODO: this class will change a lot once we have database!
        public static void RegisterNewUser(RegistrationCredentials credentials)
        {
            AddUser(new User()
            {
                Username = credentials.Username.ToLower(),
                Password = credentials.Password.Encrypt(),
                Email = credentials.Email.ToLower(),
                Fullname = credentials.Fullname
            });
        }

        //TODO: Method won't be needed once we have database!
        public static List<User> GetExistingUsers()
        {
            using (DataBaseModel context = new DataBaseModel())
            {
                return context.Set<UserDataModel>()
                    .Select(x => new User() { Email = x.Email, Username = x.Username, Fullname = x.FullName, Password = x.Password })
                    .ToList();
            }
        }
        public static void AddUser(User user)
        {
            using (DataBaseModel context = new DataBaseModel())
            {
                context.Set<UserDataModel>()
                    .Add(new UserDataModel() { Username = user.Username, Email = user.Email, Password = user.Password, FullName = user.Fullname });
                context.SaveChanges();
            }
        }
    }
}
