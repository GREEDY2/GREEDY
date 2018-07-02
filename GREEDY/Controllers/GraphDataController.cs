using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using GREEDY.DataManagers;
using GREEDY.Extensions;
using GREEDY.Services;

namespace GREEDY.Controllers
{
    [EnableCors("*", "*", "*")]
    public class GraphDataController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IGraphManager _graphManager;

        public GraphDataController(IAuthenticationService authenticationService, IGraphManager graphManager)
        {
            _authenticationService = authenticationService;
            _graphManager = graphManager;
        }

        public async Task<HttpResponseMessage> Get(int id)
        {
            Request.RegisterForDispose((IDisposable) _graphManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token, out var username);
            var time = id;
            if (await isAuthenticated)
            {
                var graphData = _graphManager.GetDataForGraphs(username, time);
                return HelperClass.JsonHttpResponse(graphData);
            }

            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
    }
}