using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using GREEDY.DataManagers;
using GREEDY.Extensions;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GREEDY.Models;

namespace GREEDY.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GetItemsFromPostedReceiptController : ApiController
    {
        private IItemManager _itemManager = new ItemManager();
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var list = _itemManager.GetItemsOfSingleReceipt(id);
                return HelperClass.JsonHttpResponse(list);
            }
            catch (NullReferenceException)
            {
                return HelperClass.JsonHttpResponse<Object>(null);
            }
        }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UpdateItemController : ApiController
    {
        private IItemManager _itemManager = new ItemManager();
        public async Task<HttpResponseMessage> Put()
        {
            HttpContent content = Request.Content;
            string jsonContent = await content.ReadAsStringAsync();
            var updatedItem = JsonConvert.DeserializeObject<Item>(jsonContent);
            _itemManager.UpdateItem(updatedItem);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}
