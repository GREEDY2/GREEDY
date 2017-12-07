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
        private IReceiptManager _receiptManager;
        public GetItemsFromPostedReceiptController(IItemManager itemManager, 
            IAuthenticationService authenticationService, IReceiptManager receiptManager)
        {
            _itemManager = itemManager;
            _authenticationService = authenticationService;
            _receiptManager = receiptManager;
        }
        public async Task<HttpResponseMessage> Get(int id)
        {
            Request.RegisterForDispose((IDisposable)_itemManager);
            Request.RegisterForDispose((IDisposable)_receiptManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token);
            if (await isAuthenticated)
            {
                try
                {
                    var list = _itemManager.GetItemsOfSingleReceipt(id);
                    var receiptInfo = _receiptManager.GetReceipt(id);
                    var response = new
                    {
                        list,
                        shopName = receiptInfo.Shop != null? receiptInfo.Shop.Name : null,
                        shopAdress = receiptInfo.Shop != null? receiptInfo.Shop.Address : null,
                        receiptDate = receiptInfo.ReceiptDate,
                        total = receiptInfo.Total
                    };
                    if (list == null || list.Count == 0)
                    {
                        return HelperClass.JsonHttpResponse<Object>(null);
                    }
                    return HelperClass.JsonHttpResponse(response);
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

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DeleteItemController : ApiController
    {
        private IItemManager _itemManager;
        private IAuthenticationService _authenticationService;
        public DeleteItemController(IItemManager itemManager, IAuthenticationService authenticationService)
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
            var itemToDeleteId = JsonConvert.DeserializeObject<int>(jsonContent);
            if (await isAuthenticated)
            {
                _itemManager.DeleteItem(itemToDeleteId);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AddItemController : ApiController
    {
        private IItemManager _itemManager;
        private IAuthenticationService _authenticationService;
        public AddItemController(IItemManager itemManager, IAuthenticationService authenticationService)
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
            var parsedJson = JsonConvert.DeserializeAnonymousType(jsonContent,
                new { Name = "", Price = 0, ReceiptId = 0, Category = "" });
            var itemToAdd = new Item()
            { Name = parsedJson.Name, Category = parsedJson.Category, Price = parsedJson.Price };
            if (await isAuthenticated)
            {
                _itemManager.AddItem(itemToAdd, parsedJson.ReceiptId);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }
}
