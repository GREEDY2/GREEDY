using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using GREEDY.DataManagers;
using GREEDY.Extensions;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GREEDY.Models;
using GREEDY.Services;

namespace GREEDY.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GetItemsFromPostedReceiptController : ApiController
    {
        private IItemManager _itemManager;
        private IAuthenticationService _authenticationService;
        public GetItemsFromPostedReceiptController(IItemManager itemManager, IAuthenticationService authenticationService)
        {
            _itemManager = itemManager;
            _authenticationService = authenticationService;
        }
        public async Task<HttpResponseMessage> Get(int id)
        {
            Request.RegisterForDispose((IDisposable)_itemManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token);
            if (await isAuthenticated)
            {
                try
                {
                    var list = _itemManager.GetItemsOfSingleReceipt(id);
                    if (list == null || list.Count == 0)
                    {
                        return HelperClass.JsonHttpResponse<Object>(null);
                    }
                    return HelperClass.JsonHttpResponse(list);
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

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GetAllUserItemsController : ApiController
    {
        private IItemManager _itemManager;
        private IAuthenticationService _authenticationService;
        public GetAllUserItemsController(IItemManager itemManager, IAuthenticationService authenticationService)
        {
            _itemManager = itemManager;
            _authenticationService = authenticationService;
        }
        public async Task<HttpResponseMessage> Get()
        {
            Request.RegisterForDispose((IDisposable)_itemManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token, out string username);
            if (await isAuthenticated)
            {
                try
                {
                    var items = _itemManager.GetAllUserItems(username);
                    return HelperClass.JsonHttpResponse(items);
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

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UpdateItemController : ApiController
    {
        private IItemManager _itemManager;
        private IAuthenticationService _authenticationService;
        public UpdateItemController(IItemManager itemManager, IAuthenticationService authenticationService)
        {
            _itemManager = itemManager;
            _authenticationService = authenticationService;
        }
        public async Task<HttpResponseMessage> Put()
        {
            Request.RegisterForDispose((IDisposable)_itemManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token);
            HttpContent content = Request.Content;
            string jsonContent = await content.ReadAsStringAsync();
            var updatedItem = JsonConvert.DeserializeObject<Item>(jsonContent);
            if (await isAuthenticated)
            {
                _itemManager.UpdateItem(updatedItem);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }
}
