using GREEDY.Extensions;
using GREEDY.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GREEDY.DataManagers
{
    public static class UserRegistration
    {
        //TODO: this class will change a lot once we have database
        public static void RegisterNewUser(RegistrationCredentials credentials)
        {
            var users = GetExistingUsers();
            users.Add(new User()
            {
                Username = credentials.Username.ToLower(),
                Password = credentials.Password.Encrypt(),
                Email = credentials.Email.ToLower(),
                Fullname = credentials.Fullname
            }
                );
            SaveUsers(users);
        }

        //TODO: Method won't be needed once we have database
        private static List<User> GetExistingUsers()
        {
            List<User> users = null;
            if (!File.Exists(Environments.AppConfig.UsersDataPath))
            {
                //File.Create(Environments.AppConfig.UsersDataPath);
                // TO DO:Fix problem with file creation
            }
            else
            {
                users = JsonConvert.DeserializeObject<List<User>>
                    (File.ReadAllText(Environments.AppConfig.UsersDataPath));
            }
            if (users == null)
            {
                users = new List<User>();
            }
            return users;
        }

        //TODO: This method will have to save to database
        private static void SaveUsers(List<User> users)
        {
            File.WriteAllText(Environments.AppConfig.UsersDataPath, JsonConvert.SerializeObject(users));
        }
    }
}
