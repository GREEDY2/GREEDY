using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GREEDY.OCRs;
using System.IO;
using System.Web.Http;
using System.Web.Http.Cors;
using GREEDY.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using GREEDY.Models;
using System.Text;

namespace GREEDY.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ImagePostController : ApiController
    {
        public HttpResponseMessage Put()
        {
            var requestStream = Request.Content.ReadAsStreamAsync().Result;
            var memoryStream = new MemoryStream(); //Using a MemoryStream because can't parse directly to image
            requestStream.CopyTo(memoryStream);
            requestStream.Close();
            var receiptImage = new Bitmap(memoryStream);
            memoryStream.Close();
            var list = new ReceiptService().ProcessReceiptImage(receiptImage);
            //TODO: Need to save the items somewhere
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
