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

        public async Task<HttpResponseMessage> Put()
        {
            Request.RegisterForDispose((IDisposable) _graphManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token, out string username);
            var time = 3600;
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

    [EnableCors(origins: "*", headers: "*", methods: "*")]
        public class AverageStorePriceGraphDataController : ApiController
        {
            private IAuthenticationService _authenticationService;
            private IGraphManager _graphManager;
            public AverageStorePriceGraphDataController(IAuthenticationService authenticationService, IGraphManager graphManager)
            {
                _authenticationService = authenticationService;
                _graphManager = graphManager;
            }
            public async Task<HttpResponseMessage> Put()
            {
                Request.RegisterForDispose((IDisposable)_graphManager);
                var token = Request.Headers.Authorization.Parameter;
                var isAuthenticated = _authenticationService.ValidateToken(token, out string username);
                var time = 3600;
                if (await isAuthenticated)
                {
                    var graphData = _graphManager.GetDataForAverageStorePriceGraph(username, time);
                    return HelperClass.JsonHttpResponse(graphData);
                }
                else
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                }
            }
        }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MostBoughtItemsGraphDataController : ApiController
    {
        private IAuthenticationService _authenticationService;
        private IGraphManager _graphManager;
        public MostBoughtItemsGraphDataController(IAuthenticationService authenticationService, IGraphManager graphManager)
        {
            _authenticationService = authenticationService;
            _graphManager = graphManager;
        }
        public async Task<HttpResponseMessage> Put()
        {
            Request.RegisterForDispose((IDisposable)_graphManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token, out string username);
            var time = 3600;
            if (await isAuthenticated)
            {
                var graphData = _graphManager.GetDataForMostBoughtItemsGraph(username, time);
                return HelperClass.JsonHttpResponse(graphData);
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ShopVisitCountGraphDataController : ApiController
    {
        private IAuthenticationService _authenticationService;
        private IGraphManager _graphManager;
        public ShopVisitCountGraphDataController(IAuthenticationService authenticationService, IGraphManager graphManager)
        {
            _authenticationService = authenticationService;
            _graphManager = graphManager;
        }
        public async Task<HttpResponseMessage> Put()
        {
            Request.RegisterForDispose((IDisposable)_graphManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token, out string username);
            var time = 3600;
            if (await isAuthenticated)
            {
                var graphData = _graphManager.GetDataForShopVisitCountGraph(username, time);
                return HelperClass.JsonHttpResponse(graphData);
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ShopItemCountGraphDataController : ApiController
    {
        private IAuthenticationService _authenticationService;
        private IGraphManager _graphManager;
        public ShopItemCountGraphDataController(IAuthenticationService authenticationService, IGraphManager graphManager)
        {
            _authenticationService = authenticationService;
            _graphManager = graphManager;
        }
        public async Task<HttpResponseMessage> Put()
        {
            Request.RegisterForDispose((IDisposable)_graphManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token, out string username);
            var time = 3600;
            if (await isAuthenticated)
            {
                var graphData = _graphManager.GetDataForShopItemCountGraph(username, time);
                return HelperClass.JsonHttpResponse(graphData);
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WeekVisitsGraphDataController : ApiController
    {
        private IAuthenticationService _authenticationService;
        private IGraphManager _graphManager;
        public WeekVisitsGraphDataController(IAuthenticationService authenticationService, IGraphManager graphManager)
        {
            _authenticationService = authenticationService;
            _graphManager = graphManager;
        }
        public async Task<HttpResponseMessage> Put()
        {
            Request.RegisterForDispose((IDisposable)_graphManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token, out string username);
            var time = 3600;
            if (await isAuthenticated)
            {
                var graphData = _graphManager.GetDataForWeekVisitsGraph(username, time);
                return HelperClass.JsonHttpResponse(graphData);
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }
}