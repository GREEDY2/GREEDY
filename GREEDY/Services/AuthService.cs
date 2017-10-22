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
        //TODO: This mothed will be useless with database, implement one of two options:
        //1. The controller has an object of the database info and it passes it to the service.
        //2. The service has an object of the database, constantly updates it.
        private static void UpdateUsers()
        {
            _users = GREEDY.DataManagers.UserManager.GetExistingUsers();
        }

        public static User FindByUsername(string username)
        {
            UpdateUsers();
            return _users.FirstOrDefault(user => user.Username.ToLower() == username.ToLower());
        }

        public static User FindByEmail(string email)
        {
            UpdateUsers();
            return _users.FirstOrDefault(user => user.Email.ToLower() == email.ToLower());
        }

        public static User FindById(int id)
        {
            UpdateUsers();
            return _users.FirstOrDefault(user => user.Id == id);
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
