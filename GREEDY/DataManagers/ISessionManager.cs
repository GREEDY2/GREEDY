using GREEDY.Models;

namespace GREEDY.DataManagers
{
    public interface ISessionManager
    {
        LoginSession CreateNewSession(User user);
        bool AuthenticateSession(LoginSession session);
    }
}
