using GREEDY.Models;
using GREEDY.Data;
using System.Linq;
using System;
using System.Data.Entity;

namespace GREEDY.DataManagers
{
    public class SessionManager : ISessionManager, IDisposable
    {
        private DbContext context;
        public SessionManager(DbContext context)
        {
            this.context = context;
        }
        public LoginSession CreateNewSession(User user)
        {
            var userDataModel = context.Set<UserDataModel>()
                .FirstOrDefault(x => x.Username == user.Username);

            var loginSessionDataModel = context.Set<LoginSessionDataModel>()
                .Add(new LoginSessionDataModel()
                {
                    SessionID = Guid.NewGuid(),
                    User = userDataModel
                });
            context.SaveChanges();
            return new LoginSession()
            {
                SessionID = loginSessionDataModel.SessionID,
                Username = userDataModel.Username
            };
        }

        public bool AuthenticateSession(LoginSession session)
        {
            {
                var loginSessionDataModel = context.Set<LoginSessionDataModel>()
                    .FirstOrDefault(x => x.SessionID == session.SessionID &&
                    x.User.Username == session.Username);
                if (loginSessionDataModel != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
