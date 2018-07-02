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
    public class ShopsController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IShopManager _shopManager;

        public ShopsController(IShopManager shopManager, IAuthenticationService authenticationService)
        {
            _shopManager = shopManager;
            _authenticationService = authenticationService;
        }

        public async Task<HttpResponseMessage> Get()
        {
            Request.RegisterForDispose((IDisposable) _shopManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token, out var username);
            if (!await isAuthenticated) return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            try
            {
                var shops = _shopManager.GetAllUserShops(username);
                return HelperClass.JsonHttpResponse(shops);
            }
            catch (NullReferenceException)
            {
                return HelperClass.JsonHttpResponse<object>(null);
            }
        }
    }
}