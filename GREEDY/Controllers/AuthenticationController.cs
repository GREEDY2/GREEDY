using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GREEDY.OCRs;
using System.IO;
using System.Web.Http;
using System.Web.Http.Cors;
using GREEDY.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using GREEDY.Models;
using GREEDY.Extensions;
using System.Text;
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
            Credentials credentials = JsonConvert.DeserializeObject<Credentials>(jsonContent);
            if (credentials.username.Length < 1 || credentials.password.Length < 1
                || credentials.username.Length > 256 || credentials.password.Length > 256)
            {
                return HelperClass.JsonHttpResponse<Object>(null);
            }
            User user = AuthService.FindByUsername(credentials.username);
            if (user == null)
                user = AuthService.FindByEmail(credentials.username);
            if (user == null)
                return HelperClass.JsonHttpResponse<Object>(null);
            if (user.password.Decrypt() == credentials.password)
            {
                user.password = null;
                return HelperClass.JsonHttpResponse(user);
            }
            /*
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
