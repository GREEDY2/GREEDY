using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Facebook;
using GREEDY.DataManagers;
using GREEDY.Extensions;
using GREEDY.Models;
using GREEDY.Services;
using Newtonsoft.Json;

namespace GREEDY.Controllers
{
    [EnableCors("*", "*", "*")]
    public class LoginController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserManager _userManager;

        public LoginController(IUserManager userManager, IAuthenticationService authenticationService)
        {
            _userManager = userManager;
            _authenticationService = authenticationService;
        }

        public async Task<HttpResponseMessage> Put()
        {
            Request.RegisterForDispose((IDisposable) _userManager);
            var requestContent = Request.Content;
            var jsonContent = await requestContent.ReadAsStringAsync();
            var credentials = JsonConvert.DeserializeObject<LoginCredentials>(jsonContent);
            if (!credentials.Username.IsUsernameValid()
                || !credentials.Password.IsPasswordValid())
                return HelperClass.JsonHttpResponse<object>(null);
            var user = _userManager.FindByUsername(credentials.Username, false) 
                       ?? _userManager.FindByEmail(credentials.Username, false);
            if (user == null) return HelperClass.JsonHttpResponse<object>(null);
            if (user.Password.Decrypt() == credentials.Password)
            {
                var token = _authenticationService.GenerateToken(credentials.Username);
                return HelperClass.JsonHttpResponse(token);
            }

            return HelperClass.JsonHttpResponse<object>(null);
        }
    }

    [EnableCors("*", "*", "*")]
    public class LoginFBController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserManager _userManager;

        public LoginFBController(IUserManager userManager, IAuthenticationService authenticationService)
        {
            _userManager = userManager;
            _authenticationService = authenticationService;
        }

        public async Task<HttpResponseMessage> Put()
        {
            Request.RegisterForDispose((IDisposable) _userManager);
            var requestContent = Request.Content;
            var jsonContent = await requestContent.ReadAsStringAsync();
            var fbUser = JsonConvert.DeserializeAnonymousType(jsonContent,
                new
                {
                    email = "",
                    accessToken = "",
                    name = ""
                }
            );
            var email = fbUser.email;
            var fbclient = new FacebookClient
            {
                AccessToken = fbUser.accessToken
            };
            var me = fbclient.Get<FbUserModel>("me?fields=email");
            if (email != me.email)
                return HelperClass.JsonHttpResponse<object>(null);

            var user = _userManager.FindByEmail(email, true);
            if (user == null)
                _userManager.AddUser(user = new User
                {
                    Username = fbUser.name,
                    Email = fbUser.email,
                    Fullname = fbUser.name,
                    IsFacebookUser = true
                });
            var token = _authenticationService.GenerateToken(user.Username);
            return HelperClass.JsonHttpResponse(token);
        }

        private class FbUserModel
        {
#pragma warning disable 0649
            public string email;
            public string id;
#pragma warning restore 0649
        }
    }

    [EnableCors("*", "*", "*")]
    public class RegistrationController : ApiController
    {
        private readonly IUserManager _userManager;

        public RegistrationController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<HttpResponseMessage> Post()
        {
            Request.RegisterForDispose((IDisposable) _userManager);
            var requestContent = Request.Content;
            var jsonContent = await requestContent.ReadAsStringAsync();
            var credentials = JsonConvert.DeserializeObject<RegistrationCredentials>(jsonContent);
            if (!credentials.Username.IsUsernameValid()
                || !credentials.Password.IsPasswordValid()
                || !credentials.Email.IsEmailValid())
                return HelperClass.JsonHttpResponse<object>(null);
            if (_userManager.FindByUsername(credentials.Username, false) != null
                || _userManager.FindByUsername(credentials.Username, true) != null)
                return HelperClass.JsonHttpResponse<object>(null);
            if (_userManager.FindByEmail(credentials.Email, false) != null
                || _userManager.FindByEmail(credentials.Email, true) != null)
                return HelperClass.JsonHttpResponse<object>(null);
            _userManager.RegisterNewUser(credentials);
            return HelperClass.JsonHttpResponse(true);
        }
    }

    [EnableCors("*", "*", "*")]
    public class ChangeEmailController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserManager _userManager;

        public ChangeEmailController(IUserManager userManager, IAuthenticationService authenticationService)
        {
            _userManager = userManager;
            _authenticationService = authenticationService;
        }

        public async Task<HttpResponseMessage> Put()
        {
            Request.RegisterForDispose((IDisposable) _userManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token, out var username);
            var jsonContent = await Request.Content.ReadAsStringAsync();
            var passwordAndEmailObject = JsonConvert.DeserializeAnonymousType(jsonContent,
                new {password = "", email = ""});
            var password = passwordAndEmailObject.password;
            var newEmail = passwordAndEmailObject.email;
            if (await isAuthenticated)
            {
                    //Email is inccorect
                    //TODO: need a different message for the user if this happens
                if (!newEmail.IsEmailValid()) return HelperClass.JsonHttpResponse<object>(null);
                if (_userManager.FindByEmail(newEmail, false) != null)
                    //Email is already taken
                    //TODO: need a different message for the user if this happens
                    return HelperClass.JsonHttpResponse<object>(null);
                var isUserMatched = _userManager.ChangeUserEmail(username, password.Encrypt(), newEmail);
                if (isUserMatched)
                    return new HttpResponseMessage(HttpStatusCode.OK);
                return HelperClass.JsonHttpResponse<object>(null);
            }

            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
    }

    [EnableCors("*", "*", "*")]
    public class ChangePasswordController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserManager _userManager;

        public ChangePasswordController(IUserManager userManager, IAuthenticationService authenticationService)
        {
            _userManager = userManager;
            _authenticationService = authenticationService;
        }

        public async Task<HttpResponseMessage> Put()
        {
            Request.RegisterForDispose((IDisposable) _userManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token, out var username);
            var jsonContent = await Request.Content.ReadAsStringAsync();
            var passwordAndNewPasswordObject = JsonConvert.DeserializeAnonymousType(jsonContent,
                new {password = "", newpassword = ""});
            var password = passwordAndNewPasswordObject.password;
            var newPassword = passwordAndNewPasswordObject.newpassword;
            if (!await isAuthenticated) return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            //New password is invalid
            //TODO: need a different message for the user if this happens
            if (!newPassword.IsPasswordValid()) return HelperClass.JsonHttpResponse<object>(null);
            var isUserMatched =
                _userManager.ChangeUserPassword(username, password.Encrypt(), newPassword.Encrypt());
            //Password is invalid
            //TODO: need a different message for the user if this happens
            return isUserMatched ? new HttpResponseMessage(HttpStatusCode.OK) : HelperClass.JsonHttpResponse<object>(null);
        }
    }
}