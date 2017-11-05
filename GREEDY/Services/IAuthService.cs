using GREEDY.Models;

namespace GREEDY.Services
{
    public interface IAuthService
    {
        User FindByUsername(string username);
        User FindByEmail(string email);
        User FindById(int id);
    }
}
