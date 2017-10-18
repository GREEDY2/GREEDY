using GREEDY.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace GREEDY.Services
{
    public static class AuthService
    {
        private static List<User> _users;

        static AuthService()
        {
            UpdateUsers();
        }

        private static void UpdateUsers()
        {
            if (!File.Exists(Environments.AppConfig.UsersDataPath))
            {
                //File.Create(Environments.AppConfig.CategoriesDataPath);
                // TO DO:Fix problem with file creation, 
                // line 56 throws an exeption when changing category, if file was created this way
            }
            else
            {
                _users = JsonConvert.DeserializeObject<List<User>>
                    (File.ReadAllText(Environments.AppConfig.UsersDataPath));
            }
            if (_users == null)
            {
                _users = new List<User>();
            }
        }

        public static User FindByUsername(string username)
        {
            UpdateUsers();
            foreach (var item in _users)
            {
                if (username.ToLower() == item.username.ToLower())
                {
                    return item;
                }
            }
            return null;
        }

        public static User FindByEmail(string email)
        {
            UpdateUsers();
            foreach (var item in _users)
            {
                if (email.ToLower() == item.email.ToLower())
                {
                    return item;
                }
            }
            return null;
        }

        public static User FindById(int id)
        {
            UpdateUsers();
            foreach (var item in _users)
            {
                if (id == item.id)
                {
                    return item;
                }
            }
            return null;
        }
        //TODO: Add logic for geting users session ID

        /*public static string GetRoleBySessionId(AppDbContext _context, string sessionId)
        {
            //string sessionId = Request.Cookies["sessionId"];
            if (sessionId == null)
                return "guest";
            sessionId = sessionId.Split('+')[0];
            sessionId = "\"" + sessionId + "\"";
            var allSessions = _context.Sessions.ToList();
            var session = allSessions.FirstOrDefault(x => JsonConvert.SerializeObject(x.SessionId) == sessionId);
            if (session == null)
                return "guest";
            int userId = session.UserId;
            var user = AuthService.FindById(_context, userId);
            if (user == null)
                return "guest";
            return user.role;
        }*/
    }
}
