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
                Fullname = credentials.Fullname,
                IsFacebookUser = false
            });
        }

        public List<User> GetExistingUsers()
        {
            return context.Set<UserDataModel>()
                .Select(x => new User() {
                    Email = x.Email, Username = x.Username,
                    Fullname = x.FullName, Password = x.Password })
               .ToList();
        }
        public void AddUser(User user)
        {
            context.Set<UserDataModel>()
                .Add(new UserDataModel() {
                    Username = user.Username, Email = user.Email,
                    Password = user.Password, FullName = user.Fullname,
                    IsFacebookUser =user.IsFacebookUser });
            context.SaveChanges();
        }

        public User FindByUsername(string username, bool isFacebookUser)
        {
            UserDataModel userDataModel = context.Set<UserDataModel>()
                .FirstOrDefault(user => user.Username.ToLower() == username.ToLower() 
                && user.IsFacebookUser == isFacebookUser);
            return userDataModel == null ?
                null : new User
                {
                    Username = userDataModel.Username,
                    Fullname = userDataModel.FullName,
                    Email = userDataModel.Email,
                    Password = userDataModel.Password,
                    IsFacebookUser = userDataModel.IsFacebookUser
                };
        }

        public User FindByEmail(string email, bool isFacebookUser)
        {
            UserDataModel userDataModel = context.Set<UserDataModel>()
                .FirstOrDefault(user => user.Email.ToLower() == email.ToLower() 
                && user.IsFacebookUser == isFacebookUser);
            return userDataModel == null ?
                null : new User
                {
                    Username = userDataModel.Username,
                    Fullname = userDataModel.FullName,
                    Email = userDataModel.Email,
                    Password = userDataModel.Password,
                    IsFacebookUser = userDataModel.IsFacebookUser
                };
        }

        public bool ChangeUserEmail(string username, string encryptedPassword, string newEmail)
        {
            var userToUpdate = context.Set<UserDataModel>()
                .FirstOrDefault(x => x.Username == username && x.Password == encryptedPassword);
            if (userToUpdate == null)
            {
                return false;
            }
            userToUpdate.Email = newEmail;
            context.SaveChanges();
            return true;
        }

        public bool ChangeUserPassword(string username, string encryptedPassword, string newEncryptedPassword)
        {
            var userToUpdate = context.Set<UserDataModel>()
                .FirstOrDefault(x => x.Username == username && x.Password == encryptedPassword);
            if (userToUpdate == null)
            {
                return false;
            }
            userToUpdate.Password = newEncryptedPassword;
            context.SaveChanges();
            return true;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}

