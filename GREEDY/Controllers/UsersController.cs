using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
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
        private ISessionManager _sessionManager;
        private IUserManager _userManager;
        public LoginController(ISessionManager sessionManager, IUserManager userManager)
        {
            _sessionManager = sessionManager;
            _userManager = userManager;
        }
        public async Task<HttpResponseMessage> Put()
        {
            Request.RegisterForDispose((IDisposable)_userManager);
            Request.RegisterForDispose((IDisposable)_sessionManager);
            HttpContent requestContent = Request.Content;
            string jsonContent = await requestContent.ReadAsStringAsync();
            LoginCredentials credentials = JsonConvert.DeserializeObject<LoginCredentials>(jsonContent);
            if (!credentials.Username.IsUsernameValid()
                || !credentials.Password.IsPasswordValid())
            {
                return HelperClass.JsonHttpResponse<Object>(null);
            }
            User user = _userManager.FindByUsername(credentials.Username);
            if (user == null)
            {
                user = _userManager.FindByEmail(credentials.Username);
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
        private IUserManager _userManager;
        public RegistrationController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        public async Task<HttpResponseMessage> Post()
        {
            Request.RegisterForDispose((IDisposable)_userManager);
            HttpContent requestContent = Request.Content;
            string jsonContent = await requestContent.ReadAsStringAsync();
            RegistrationCredentials credentials = JsonConvert.DeserializeObject<RegistrationCredentials>(jsonContent);
            if (!credentials.Username.IsUsernameValid()
                || !credentials.Password.IsPasswordValid()
                || !credentials.Email.IsEmailValid())
            {
                return HelperClass.JsonHttpResponse<Object>(null);
            }
            if (_userManager.FindByUsername(credentials.Username) != null)
            {
                return HelperClass.JsonHttpResponse<Object>(null);
            }
            if (_userManager.FindByEmail(credentials.Email) != null)
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
            Request.RegisterForDispose((IDisposable)_sessionManager);
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

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ChangeEmailController : ApiController
    {
        private IUserManager _userManager;
        public ChangeEmailController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        public async Task<HttpResponseMessage> Put()
        {
            Request.RegisterForDispose((IDisposable)_userManager);
            HttpContent requestContent = Request.Content;
            string jsonContent = await requestContent.ReadAsStringAsync();
            string username = "";
            string newEmail = "";
            //TODO: dynamicly get username and new email.

            //TODO: this now checks if this user exist. Actually we need to try and authorize with each request
            //so if the user is succesfully authorized we don't really need to check if that user exists.
            User user = _userManager.FindByUsername(username);
            if (user == null)
            {
                return HelperClass.JsonHttpResponse<Object>(null);
            }
            user = _userManager.FindByEmail(newEmail);
            if (user != null)
            {
                //Email is already taken
                //TODO: need a different message for the user if this happens
                return HelperClass.JsonHttpResponse<Object>(null);
            }
            //TODO: write this method (this class has too many todos and faults so will do everything at one time
            //_userManager.ChangeUserEmail(username, email);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ChangePasswordController : ApiController
    {
        private IUserManager _userManager;
        public ChangePasswordController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        public async Task<HttpResponseMessage> Put()
        {
            Request.RegisterForDispose((IDisposable)_userManager);
            HttpContent requestContent = Request.Content;
            string jsonContent = await requestContent.ReadAsStringAsync();
            string username = "";
            string newPassword = "";
            if (!newPassword.IsPasswordValid())
            {
                return HelperClass.JsonHttpResponse<Object>(null);
            }
            //TODO: dynamicly get username and new password.

            //TODO: this now checks if this user exist. Actually we need to try and authorize with each request
            //so if the user is succesfully authorized we don't really need to check if that user exists.
            User user = _userManager.FindByUsername(username);
            if (user == null)
            {
                return HelperClass.JsonHttpResponse<Object>(null);
            }

            //TODO: write this method (this class has too many todos and faults so will do everything at one time
            //_userManager.ChangeUserPassword(username, email);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}
