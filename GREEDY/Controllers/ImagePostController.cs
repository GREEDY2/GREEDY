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
        private IAuthenticationService _authenticationService;
        public ImageUploadController(IItemManager itemManager, IReceiptService receiptService,
            IAuthenticationService authenticationService)
        {
            _itemManager = itemManager;
            _receiptService = receiptService;
            _authenticationService = authenticationService;
        }

        public async Task<HttpResponseMessage> Post()
        {
            Request.RegisterForDispose((IDisposable)_itemManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token, out string username);
            var requestStream = await Request.Content.ReadAsStreamAsync();
            var memoryStream = new MemoryStream(); //Using a MemoryStream because can't parse directly to image
            requestStream.CopyTo(memoryStream);
            requestStream.Close();
            var receiptImage = new Bitmap(memoryStream);
            memoryStream.Close();
            var receipt = _receiptService.ProcessReceiptImage(receiptImage);

            
            if (await isAuthenticated)
            {
                var receiptId = _itemManager.AddItems(receipt, username);
                return HelperClass.JsonHttpResponse(receiptId);
                //TODO: create an error if something goes wrong
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }
}
