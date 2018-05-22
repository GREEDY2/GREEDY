using GREEDY.Extensions;

namespace GREEDY.Models
{
    public class LoginCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool Verify()
        {
            return Username.IsUsernameValid() && Password.IsPasswordValid();
        }
    }
}
