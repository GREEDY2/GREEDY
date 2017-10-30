using GREEDY.Models;
using GREEDY.Data;
using System.Linq;
using System;

namespace GREEDY.DataManagers
{
    public class SessionManager : ISessionManager
    {
        public LoginSession CreateNewSession(User user)
        {
            using (DataBaseModel context = new DataBaseModel())
            {
                var userDataModel = context.Set<UserDataModel>()
                    .FirstOrDefault(x => x.Username == user.Username);

                var loginSessionDataModel = context.Set<LoginSessionDataModel>()
                    .Add(new LoginSessionDataModel() { SessionID = Guid.NewGuid(),
                        User = userDataModel });
                return new LoginSession() { SessionID = loginSessionDataModel.SessionID,
                    Username = userDataModel.Username};
            }
        }

        public bool AuthenticateSession(LoginSession session)
        {
            using (DataBaseModel context = new DataBaseModel())
            {

            }
            return true;
        }
    }
}
