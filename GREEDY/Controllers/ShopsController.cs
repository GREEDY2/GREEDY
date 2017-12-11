using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Net.Http;
using GREEDY.Extensions;
using GREEDY.DataManagers;
using GREEDY.Services;

namespace GREEDY.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ShopsController : ApiController
    {
        private IShopManager _shopManager;
        private IAuthenticationService _authenticationService;
        public ShopsController(IShopManager shopManager, IAuthenticationService authenticationService)
        {
            _shopManager = shopManager;
            _authenticationService = authenticationService;
        }
        public async Task<HttpResponseMessage> Get()
        {
            Request.RegisterForDispose((IDisposable)_shopManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token, out string username);
            if (await isAuthenticated)
            {
                try
                {
                    var shops = _shopManager.GetAllUserShops(username);
                    return HelperClass.JsonHttpResponse(shops);
                }
                catch (NullReferenceException)
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
}