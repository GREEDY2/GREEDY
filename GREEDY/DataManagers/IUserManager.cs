﻿using GREEDY.Models;
using System.Collections.Generic;

namespace GREEDY.DataManagers
{
    public interface IUserManager
    {
        void RegisterNewUser(RegistrationCredentials credentials);
        List<User> GetExistingUsers();
        void AddUser(User user);
        User FindByUsername(string username);
        User FindByEmail(string email);
        bool ChangeUserEmail(string username, string encryptedPassword, string newEmail);
        bool ChangeUserPassword(string username, string encryptedPassword, string newEncryptedPassword);
    }
}
