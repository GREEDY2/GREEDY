using GREEDY.Extensions;
using GREEDY.Models;
using System.Collections.Generic;
using GREEDY.Data;
using System.Linq;
using System;
using System.Data.Entity;

namespace GREEDY.DataManagers
{
    public class UserManager : IUserManager, IDisposable
    {
        private DbContext context;

        public UserManager(DbContext context)
        {
            this.context = context;
        }

        public void RegisterNewUser(RegistrationCredentials credentials)
        {
            AddUser(new User()
            {
                Username = credentials.Username.ToLower(),
                Password = credentials.Password.Encrypt(),
                Email = credentials.Email.ToLower(),
                Fullname = credentials.Fullname
            });
        }

        public List<User> GetExistingUsers()
        {
            return context.Set<UserDataModel>()
                .Select(x => new User() { Email = x.Email, Username = x.Username, Fullname = x.FullName, Password = x.Password })
               .ToList();
        }
        public void AddUser(User user)
        {
            context.Set<UserDataModel>()
                .Add(new UserDataModel() { Username = user.Username, Email = user.Email, Password = user.Password, FullName = user.Fullname });
            context.SaveChanges();
        }

        public User FindByUsername(string username)
        {
            UserDataModel userDataModel = context.Set<UserDataModel>().FirstOrDefault(user => user.Username.ToLower() == username.ToLower());
            return userDataModel == null ?
                null : new User
                {
                    Username = userDataModel.Username,
                    Fullname = userDataModel.FullName,
                    Email = userDataModel.Email,
                    Password = userDataModel.Password
                };
        }

        public User FindByEmail(string email)
        {
            UserDataModel userDataModel = context.Set<UserDataModel>().FirstOrDefault(user => user.Email.ToLower() == email.ToLower());
            return userDataModel == null ?
                null : new User
                {
                    Username = userDataModel.Username,
                    Fullname = userDataModel.FullName,
                    Email = userDataModel.Email,
                    Password = userDataModel.Password
                };
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}

