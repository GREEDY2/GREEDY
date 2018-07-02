using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GREEDY.Data;
using GREEDY.Extensions;
using GREEDY.Models;

namespace GREEDY.DataManagers
{
    public class UserManager : IUserManager, IDisposable
    {
        private readonly DbContext _context;

        public UserManager(DbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void RegisterNewUser(RegistrationCredentials credentials)
        {
            AddUser(new User
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
            return _context.Set<UserDataModel>()
                .Select(x => new User
                {
                    Email = x.Email,
                    Username = x.Username,
                    Fullname = x.FullName,
                    Password = x.Password
                })
                .ToList();
        }

        public void AddUser(User user)
        {
            _context.Set<UserDataModel>()
                .Add(new UserDataModel
                {
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password,
                    FullName = user.Fullname,
                    IsFacebookUser = user.IsFacebookUser
                });
            _context.SaveChanges();
        }

        public User FindByUsername(string username, bool isFacebookUser)
        {
            var userDataModel = _context.Set<UserDataModel>()
                .FirstOrDefault(user => user.Username.ToLower() == username.ToLower()
                                        && user.IsFacebookUser == isFacebookUser);
            return userDataModel == null
                ? null
                : new User
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
            var userDataModel = _context.Set<UserDataModel>()
                .FirstOrDefault(user => user.Email.ToLower() == email.ToLower()
                                        && user.IsFacebookUser == isFacebookUser);
            return userDataModel == null
                ? null
                : new User
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
            var userToUpdate = _context.Set<UserDataModel>()
                .FirstOrDefault(x => x.Username == username && x.Password == encryptedPassword);
            if (userToUpdate == null) return false;
            userToUpdate.Email = newEmail;
            _context.SaveChanges();
            return true;
        }

        public bool ChangeUserPassword(string username, string encryptedPassword, string newEncryptedPassword)
        {
            var userToUpdate = _context.Set<UserDataModel>()
                .FirstOrDefault(x => x.Username == username && x.Password == encryptedPassword);
            if (userToUpdate == null) return false;
            userToUpdate.Password = newEncryptedPassword;
            _context.SaveChanges();
            return true;
        }
    }
}