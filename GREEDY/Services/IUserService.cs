using GREEDY.Models;

namespace GREEDY.Services
{
    public interface IUserService
    {
        User LoginByUsernameOrEmail(LoginCredentials credentials);
    }
}
