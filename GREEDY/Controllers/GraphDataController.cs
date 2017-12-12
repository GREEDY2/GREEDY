using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using GREEDY.DataManagers;
using GREEDY.Extensions;
using System;
using GREEDY.Services;
using System.Threading.Tasks;

namespace GREEDY.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GraphDataController : ApiController
    {
        private IAuthenticationService _authenticationService;
        private IGraphManager _graphManager;

        public GraphDataController(IAuthenticationService authenticationService, IGraphManager graphManager)
        {
            _authenticationService = authenticationService;
            _graphManager = graphManager;
        }

        public async Task<HttpResponseMessage> Get(int id)
        {
            Request.RegisterForDispose((IDisposable) _graphManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token, out string username);
            var time = id;
            if (await isAuthenticated)
            {
                var graphData = _graphManager.GetDataForGraphs(username, time);
                return HelperClass.JsonHttpResponse(graphData);
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }
}