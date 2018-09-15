using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using GREEDY.DataManagers;
using GREEDY.Extensions;
using GREEDY.Models;
using GREEDY.Services;
using Newtonsoft.Json;

namespace GREEDY.Controllers
{
    [EnableCors("*", "*", "*")]
    public class GetItemsFromPostedReceiptController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IItemManager _itemManager;
        private readonly IReceiptManager _receiptManager;

        public GetItemsFromPostedReceiptController(IItemManager itemManager,
            IAuthenticationService authenticationService, IReceiptManager receiptManager)
        {
            _itemManager = itemManager;
            _authenticationService = authenticationService;
            _receiptManager = receiptManager;
        }

        public async Task<HttpResponseMessage> Get(int id)
        {
            Request.RegisterForDispose((IDisposable) _itemManager);
            Request.RegisterForDispose((IDisposable) _receiptManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token);
            if (!await isAuthenticated) return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            try
            {
                var list = _itemManager.GetItemsOfSingleReceipt(id);
                var receiptInfo = _receiptManager.GetReceipt(id);
                var response = new
                {
                    list,
                    shopName = receiptInfo.Shop?.Name,
                    shopAdress = receiptInfo.Shop?.Address,
                    receiptDate = receiptInfo.ReceiptDate.HasValue
                        ? receiptInfo.ReceiptDate.Value.ToString("d")
                        : null,
                    total = receiptInfo.Total
                };
                    return (list == null || list.Count == 0)
                        ? HelperClass.JsonHttpResponse<object> (null) 
                        : HelperClass.JsonHttpResponse (response);
            }
            catch (NullReferenceException)
            {
                return HelperClass.JsonHttpResponse<object>(null);
            }
        }
    }

    [EnableCors("*", "*", "*")]
    public class GetAllUserItemsController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IItemManager _itemManager;

        public GetAllUserItemsController(IItemManager itemManager, IAuthenticationService authenticationService)
        {
            _itemManager = itemManager;
            _authenticationService = authenticationService;
        }

        public async Task<HttpResponseMessage> Get()
        {
            Request.RegisterForDispose((IDisposable) _itemManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token, out var username);
            if (!await isAuthenticated) return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            try
            {
                var items = _itemManager.GetAllUserItems(username);
                return HelperClass.JsonHttpResponse(items);
            }
            catch (NullReferenceException)
            {
                return HelperClass.JsonHttpResponse<object>(null);
            }
        }
    }

    [EnableCors("*", "*", "*")]
    public class UpdateItemController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IItemManager _itemManager;

        public UpdateItemController(IItemManager itemManager, IAuthenticationService authenticationService)
        {
            _itemManager = itemManager;
            _authenticationService = authenticationService;
        }

        public async Task<HttpResponseMessage> Put(int id)
        {
            Request.RegisterForDispose((IDisposable) _itemManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token);
            var content = Request.Content;
            var jsonContent = await content.ReadAsStringAsync();
            var updatedItem = JsonConvert.DeserializeObject<Item>(jsonContent);
            updatedItem.ItemId = id;
            if (!await isAuthenticated) return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            _itemManager.UpdateItem(updatedItem);
            return new HttpResponseMessage(HttpStatusCode.OK);

        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            Request.RegisterForDispose((IDisposable) _itemManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token);
            if (!await isAuthenticated) return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            _itemManager.DeleteItem(id);
            return new HttpResponseMessage(HttpStatusCode.OK);

        }
    }

    [EnableCors("*", "*", "*")]
    public class AddItemController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IItemManager _itemManager;

        public AddItemController(IItemManager itemManager, IAuthenticationService authenticationService)
        {
            _itemManager = itemManager;
            _authenticationService = authenticationService;
        }

        public async Task<HttpResponseMessage> Put()
        {
            Request.RegisterForDispose((IDisposable) _itemManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token);
            var content = Request.Content;
            var jsonContent = await content.ReadAsStringAsync();
            var parsedJson = JsonConvert.DeserializeAnonymousType(jsonContent,
                new {Name = "", Price = 0, ReceiptId = 0, Category = ""});
            var itemToAdd = new Item {Name = parsedJson.Name, Category = parsedJson.Category, Price = parsedJson.Price};
            if (!await isAuthenticated) return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            _itemManager.AddItem(itemToAdd, parsedJson.ReceiptId);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}