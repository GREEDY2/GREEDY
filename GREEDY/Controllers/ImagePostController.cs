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
using System.Threading.Tasks;

namespace GREEDY.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ImagePostController : ApiController
    {
        private IItemManager _itemManager = new ItemManager();
        //TODO: Research and fix how can we implement Dependency Injection into Controllers
        //Probably will be something with adding Services to OWIN application (Ninject)
        /*public ImagePostController(IItemManager itemManager)
        {
            _itemManager = itemManager;
        }*/

        public async Task<HttpResponseMessage> Put()
        {
            var requestStream = await Request.Content.ReadAsStreamAsync();
            var username = Request.Headers.Authorization.Parameter;
            var memoryStream = new MemoryStream(); //Using a MemoryStream because can't parse directly to image
            requestStream.CopyTo(memoryStream);
            requestStream.Close();
            var receiptImage = new Bitmap(memoryStream);
            memoryStream.Close();
            var list = new ReceiptService().ProcessReceiptImage(receiptImage);

            //TODO: Need to get shop

            var receiptId = _itemManager.AddItems(list, new Models.Shop() { Name = "Not supported yet", Location= "Not supported yet" }, username);
            return HelperClass.JsonHttpResponse(receiptId);
            //TODO: create an error if something goes wrong
        }
    }
}
