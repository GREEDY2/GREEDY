using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using GREEDY.Services;
using Newtonsoft.Json;
using GREEDY.Models;
using GREEDY.Extensions;
using System;
using GREEDY.DataManagers;
using System.Threading.Tasks;

namespace GREEDY.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        private IAuthService _authService;
        private ISessionManager _sessionManager;
        public LoginController(IAuthService authService, ISessionManager sessionManager)
        {
            _authService = authService;
            _sessionManager = sessionManager;
        }
        public async Task<HttpResponseMessage> Put()
        {
            HttpContent requestContent = Request.Content;
            string jsonContent = await requestContent.ReadAsStringAsync();
            LoginCredentials credentials = JsonConvert.DeserializeObject<LoginCredentials>(jsonContent);
            if (credentials.Username.Length < 5 || credentials.Password.Length < 5
                || credentials.Username.Length > 256 || credentials.Password.Length > 256)
            {
                return HelperClass.JsonHttpResponse<Object>(null);
            }
            User user = _authService.FindByUsername(credentials.Username);
            if (user == null)
            {
                user = _authService.FindByEmail(credentials.Username);
            }
            if (user == null)
            {
                return HelperClass.JsonHttpResponse<Object>(null);
            }
            if (user.Password.Decrypt() == credentials.Password)
            {
                var loginSession = _sessionManager.CreateNewSession(user);
                return HelperClass.JsonHttpResponse(loginSession);
            }
            return HelperClass.JsonHttpResponse<Object>(null);
        }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RegistrationController : ApiController
    {
        private IAuthService _authService;
        private IUserManager _userManager;
        public RegistrationController(IAuthService authService,IUserManager userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }
        public async Task<HttpResponseMessage> Put()
        {
            //TODO: Change magic numbers to const
            HttpContent requestContent = Request.Content;
            string jsonContent = await requestContent.ReadAsStringAsync();
            RegistrationCredentials credentials = JsonConvert.DeserializeObject<RegistrationCredentials>(jsonContent);
            if (credentials.Username.Length < 5 || credentials.Password.Length < 5
                || credentials.Username.Length > 256 || credentials.Password.Length > 256
                || credentials.Email.Length < 3 || credentials.Email.Length > 256
                || credentials.Fullname.Length < 1 || credentials.Fullname.Length > 512)
            {
                return HelperClass.JsonHttpResponse<Object>(null);
            }
            if (_authService.FindByUsername(credentials.Username) != null)
            {
                return HelperClass.JsonHttpResponse<Object>(null);
            }
            if (_authService.FindByEmail(credentials.Email) != null)
            {
                return HelperClass.JsonHttpResponse<Object>(null);
            }
            _userManager.RegisterNewUser(credentials);
            return HelperClass.JsonHttpResponse(true);
        }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthenticationController : ApiController
    {
        private ISessionManager _sessionManager;
        public AuthenticationController(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }
        public async Task<HttpResponseMessage> Put()
        {
            HttpContent requestContent = Request.Content;
            string jsonContent = await requestContent.ReadAsStringAsync();
            LoginSession loginSession = JsonConvert.DeserializeObject<LoginSession>(jsonContent);
            if (_sessionManager.AuthenticateSession(loginSession))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }
}
