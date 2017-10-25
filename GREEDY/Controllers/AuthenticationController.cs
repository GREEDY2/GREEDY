using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using GREEDY.Services;
using Newtonsoft.Json;
using GREEDY.Models;
using GREEDY.Extensions;
using System;

namespace GREEDY.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthenticationController : ApiController
    {
        public HttpResponseMessage Put()
        {
            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;
            LoginCredentials credentials = JsonConvert.DeserializeObject<LoginCredentials>(jsonContent);
            if (credentials.Username.Length < 5 || credentials.Password.Length < 5
                || credentials.Username.Length > 256 || credentials.Password.Length > 256)
            {
                return HelperClass.JsonHttpResponse<Object>(null);
            }
            User user = AuthService.FindByUsername(credentials.Username);
            if (user == null)
            {
                user = AuthService.FindByEmail(credentials.Username);
            }
            if (user == null)
            {
                return HelperClass.JsonHttpResponse<Object>(null);
            }
            if (user.Password.Decrypt() == credentials.Password)
            {
                user.Password = null;
                return HelperClass.JsonHttpResponse(user);
            }
            //TODO: need to uncomment this method because we have database now
            /*
             * Later will need to add sessions (Probably when we have database)
                // var allSessions = _context.Sessions.ToList();
                // var singleSession = allSessions.FirstOrDefault(x => x.UserId == user.id);
                // if (singleSession != null)
                //     _context.Remove(singleSession);
                var session = new SessionDataModel();
                session.UserId = user.id;
                session.SessionId = DateTime.Now;
                _context.Sessions.Add(session);
                _context.SaveChanges();
                user.SessionId = session.SessionId;
                return
            }*/
            return HelperClass.JsonHttpResponse<Object>(null);
        }
    }
}
