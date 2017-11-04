using GREEDY.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using GREEDY.DataManagers;
namespace GREEDY.Services
{
    public class AuthService : IAuthService
    {
        private List<User> _users;
        private IUserManager _userManager;
        public AuthService(IUserManager userManager)
        {
            _userManager = userManager;
        }
        private void UpdateUsers()
        {
            _users = _userManager.GetExistingUsers();
        }

        public User FindByUsername(string username)
        {
            UpdateUsers();
            return _users.FirstOrDefault(user => user.Username.ToLower() == username.ToLower());
        }

        public User FindByEmail(string email)
        {
            UpdateUsers();
            return _users.FirstOrDefault(user => user.Email.ToLower() == email.ToLower());
        }

        public User FindById(int id)
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
