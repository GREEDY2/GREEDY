using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using GREEDY.Models;
using GREEDY.Extensions;
using System;
using GREEDY.DataManagers;
using System.Threading.Tasks;
using GREEDY.Services;

namespace GREEDY.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        private IUserManager _userManager;
        private IAuthenticationService _authenticationService;
        public LoginController(IUserManager userManager, IAuthenticationService authenticationService)
        {
            _userManager = userManager;
            _authenticationService = authenticationService;
        }
        public async Task<HttpResponseMessage> Put()
        {
            Request.RegisterForDispose((IDisposable)_userManager);
            HttpContent requestContent = Request.Content;
            string jsonContent = await requestContent.ReadAsStringAsync();
            LoginCredentials credentials = JsonConvert.DeserializeObject<LoginCredentials>(jsonContent);
            if (!credentials.Username.IsUsernameValid()
                || !credentials.Password.IsPasswordValid())
            {
                return HelperClass.JsonHttpResponse<Object>(null);
            }
            User user = _userManager.FindByUsername(credentials.Username,false);
            if (user == null)
            {
                user = _userManager.FindByEmail(credentials.Username,false);
            }
            if (user == null)
            {
                return HelperClass.JsonHttpResponse<Object>(null);
            }
            if (user.Password.Decrypt() == credentials.Password)
            {
                var token = _authenticationService.GenerateToken(credentials.Username);
                return HelperClass.JsonHttpResponse(token);
            }
            return HelperClass.JsonHttpResponse<Object>(null);
        }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginFBController : ApiController
    {
        private class FbUserModel
        {
            public string email;
            public string id;
        }
        private IUserManager _userManager;
        private IAuthenticationService _authenticationService;
        public LoginFBController(IUserManager userManager, IAuthenticationService authenticationService)
        {
            _userManager = userManager;
            _authenticationService = authenticationService;
        }
        public async Task<HttpResponseMessage> Put()
        {
            Request.RegisterForDispose((IDisposable)_userManager);
            HttpContent requestContent = Request.Content;
            string jsonContent = await requestContent.ReadAsStringAsync();
            var fbUser = JsonConvert.DeserializeAnonymousType(jsonContent, 
                new {
                    email = "",
                    accessToken = "",
                    name = ""
                }
            );
            string email = fbUser.email;
            Facebook.FacebookClient fbclient = new Facebook.FacebookClient()
            {
                AccessToken = (string)fbUser.accessToken
            };
            var me = fbclient.Get<FbUserModel>("me?fields=email");
            if(email!=me.email)
                return HelperClass.JsonHttpResponse<Object>(null);

            User user = _userManager.FindByEmail(email,true);
            if (user == null)
            {
                _userManager.AddUser(user = new User()
                {
                    Username = fbUser.name,
                    Email = fbUser.email,
                    Fullname = fbUser.name,
                    IsFacebookUser = true
                });
            }
            var token = _authenticationService.GenerateToken(user.Username);
            return HelperClass.JsonHttpResponse(token);
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
            if (_userManager.FindByUsername(credentials.Username,false) != null || _userManager.FindByUsername(credentials.Username, true) != null)
            {
                return HelperClass.JsonHttpResponse<Object>(null);
            }
            if (_userManager.FindByEmail(credentials.Email,false) != null || _userManager.FindByEmail(credentials.Email,true) != null)
            {
                return HelperClass.JsonHttpResponse<Object>(null);
            }
            _userManager.RegisterNewUser(credentials);
            return HelperClass.JsonHttpResponse(true);
        }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ChangeEmailController : ApiController
    {
        private IUserManager _userManager;
        private IAuthenticationService _authenticationService;
        public ChangeEmailController(IUserManager userManager, IAuthenticationService authenticationService)
        {
            _userManager = userManager;
            _authenticationService = authenticationService;
        }
        public async Task<HttpResponseMessage> Put()
        {
            Request.RegisterForDispose((IDisposable)_userManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token, out string username);
            string jsonContent = await Request.Content.ReadAsStringAsync();
            var passwordAndEmailObject = JsonConvert.DeserializeAnonymousType(jsonContent, 
                new { password = "", email = ""});
            string password = passwordAndEmailObject.password;
            string newEmail = passwordAndEmailObject.email;
            if (await isAuthenticated)
            {
                if (!newEmail.IsEmailValid())
                {
                    //Email is inccorect
                    //TODO: need a different message for the user if this happens
                    return HelperClass.JsonHttpResponse<Object>(null);
                }
                if (_userManager.FindByEmail(newEmail,false) != null)
                {
                    //Email is already taken
                    //TODO: need a different message for the user if this happens
                    return HelperClass.JsonHttpResponse<Object>(null);
                }
                var isUserMatched = _userManager.ChangeUserEmail(username, password.Encrypt(), newEmail);
                if (isUserMatched)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                }
                else
                {
                    return HelperClass.JsonHttpResponse<Object>(null);
                }
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ChangePasswordController : ApiController
    {
        private IUserManager _userManager;
        private IAuthenticationService _authenticationService;
        public ChangePasswordController(IUserManager userManager, IAuthenticationService authenticationService)
        {
            _userManager = userManager;
            _authenticationService = authenticationService;
        }
        public async Task<HttpResponseMessage> Put()
        {
            Request.RegisterForDispose((IDisposable)_userManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token, out string username);
            string jsonContent = await Request.Content.ReadAsStringAsync();
            var passwordAndNewPasswordObject = JsonConvert.DeserializeAnonymousType(jsonContent, 
                new { password = "", newpassword = "" });
            string password = passwordAndNewPasswordObject.password;
            string newPassword = passwordAndNewPasswordObject.newpassword;
            if (await isAuthenticated)
            {
                if (!newPassword.IsPasswordValid())
                {
                    //New password is invalid
                    //TODO: need a different message for the user if this happens
                    return HelperClass.JsonHttpResponse<Object>(null);
                }
                var isUserMatched = _userManager.ChangeUserPassword(username, password.Encrypt(), newPassword.Encrypt());
                if (isUserMatched)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                }
                else
                {
                    //Password is invalid
                    //TODO: need a different message for the user if this happens
                    return HelperClass.JsonHttpResponse<Object>(null);
                }
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }
}
