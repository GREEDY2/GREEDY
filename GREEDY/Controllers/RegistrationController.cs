using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using GREEDY.Services;
using Newtonsoft.Json;
using GREEDY.Models;
using GREEDY.Extensions;
using System;
using GREEDY.DataManagers;

namespace GREEDY.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RegistrationController : ApiController
    {
        public HttpResponseMessage Put()
        {
            //TODO: Change magic numbers to const
            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;
            RegistrationCredentials credentials = JsonConvert.DeserializeObject<RegistrationCredentials>(jsonContent);
            if (credentials.Username.Length < 5 || credentials.Password.Length < 5
                || credentials.Username.Length > 256 || credentials.Password.Length > 256
                || credentials.Email.Length < 3 || credentials.Email.Length > 256
                || credentials.Fullname.Length < 1 || credentials.Fullname.Length > 512)
            {
                return HelperClass.JsonHttpResponse<Object>(null);
            }
            if (AuthService.FindByUsername(credentials.Username) != null)
            {
                return HelperClass.JsonHttpResponse<Object>(null);
            }
            if (AuthService.FindByEmail(credentials.Email) != null)
            {
                return HelperClass.JsonHttpResponse<Object>(null);
            }
            UserManager.RegisterNewUser(credentials);
            return HelperClass.JsonHttpResponse(true);
        }
}
}
