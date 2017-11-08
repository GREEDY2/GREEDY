using System.Drawing;
using System.Net.Http;
using System.IO;
using System.Web.Http;
using System.Web.Http.Cors;
using GREEDY.Services;
using GREEDY.DataManagers;
using GREEDY.Extensions;
using System.Threading.Tasks;
using System;

namespace GREEDY.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ImageUploadController : ApiController
    {
        private IItemManager _itemManager;
        private IReceiptService _receiptService;
        public ImageUploadController(IItemManager itemManager, IReceiptService receiptService)
        {
            _itemManager = itemManager;
            _receiptService = receiptService;
        }

        public async Task<HttpResponseMessage> Post()
        {
            Request.RegisterForDispose((IDisposable)_itemManager);
            var requestStream = await Request.Content.ReadAsStreamAsync();
            var username = Request.Headers.Authorization.Parameter;
            var memoryStream = new MemoryStream(); //Using a MemoryStream because can't parse directly to image
            requestStream.CopyTo(memoryStream);
            requestStream.Close();
            var receiptImage = new Bitmap(memoryStream);
            memoryStream.Close();
            var list = _receiptService.ProcessReceiptImage(receiptImage);

            //TODO: Need to get shop

            var receiptId = _itemManager.AddItems(list, new Models.Shop()
                { Name = "Not supported yet", Location = "Not supported yet" }, username);
            return HelperClass.JsonHttpResponse(receiptId);
            //TODO: create an error if something goes wrong
        }
    }
}
