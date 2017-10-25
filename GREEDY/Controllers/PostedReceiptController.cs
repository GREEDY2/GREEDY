using System.Drawing;
using System.Net.Http;
using System.IO;
using System.Web.Http;
using System.Web.Http.Cors;
using GREEDY.Services;
using Newtonsoft.Json;
using System.Text;
using GREEDY.DataManagers;
using GREEDY.Extensions;
using System;

namespace GREEDY.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PostedReceiptController : ApiController
    {
        private IItemManager _itemManager = new ItemManager();
        public HttpResponseMessage Get(int id)
        {
            var list = _itemManager.GetItemsOfSingleReceipt(id);
            return HelperClass.JsonHttpResponse(list);
        }
    }
}
