using System.Threading.Tasks;

namespace GREEDY.Services
{
    public interface IAuthenticationService
    {
        Task<bool> ValidateToken(string token, out string username);
        Task<bool> ValidateToken(string token);
        string GenerateToken(string username, int expireHours = 24);
    }
}