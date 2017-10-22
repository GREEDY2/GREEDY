using System.Drawing;
using System.Net.Http;
using System.IO;
using System.Web.Http;
using System.Web.Http.Cors;
using GREEDY.Services;
using Newtonsoft.Json;
using System.Text;
using GREEDY.DataManagers;

namespace GREEDY.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ImagePostController : ApiController
    {
        private ItemManager itemManager = new ItemManager();
        public HttpResponseMessage Put()
        {
            var requestStream = Request.Content.ReadAsStreamAsync().Result;
            var memoryStream = new MemoryStream(); //Using a MemoryStream because can't parse directly to image
            requestStream.CopyTo(memoryStream);
            requestStream.Close();
            var receiptImage = new Bitmap(memoryStream);
            memoryStream.Close();
            var list = new ReceiptService().ProcessReceiptImage(receiptImage);

            //TODO: Need to get shop and need to have username of the user uploading thee image

            itemManager.AddItems(list, new Models.Shop() { Name = "Not supported yet", Location= "Not supported yet" }, "username");
            return new HttpResponseMessage()
            {
                Content = new StringContent(
                    JsonConvert.SerializeObject(list),
                    Encoding.UTF8,
                    "text/html"
                    )
            };
            //TODO: create an error if something goes wrong
        }
    }
}
