using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using GREEDY.DataManagers;
using GREEDY.Extensions;
using GREEDY.Services;
using OpenCvSharp;

namespace GREEDY.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ImageUploadController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IItemManager _itemManager;
        private readonly IReceiptService _receiptService;

        public ImageUploadController(IItemManager itemManager, IReceiptService receiptService,
            IAuthenticationService authenticationService)
        {
            _itemManager = itemManager;
            _receiptService = receiptService;
            _authenticationService = authenticationService;
        }

        public async Task<HttpResponseMessage> Post()
        {
            Request.RegisterForDispose((IDisposable) _itemManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token, out var username);
            var receiptImage = new Mat();
            using (var requestStream = await Request.Content.ReadAsStreamAsync())
            {
                //Using a MemoryStream because can't parse directly to image
                using (var memoryStream = new MemoryStream())
                {
                    requestStream.CopyTo(memoryStream);
                    Mat.FromStream(memoryStream, ImreadModes.Unchanged).CopyTo(receiptImage);
                    using (new Window("FirstPicture", WindowMode.Normal, receiptImage))
                    {
                    }
                }
            }

            var receipt = _receiptService.ProcessReceiptImage(receiptImage);


            //////need to test
            ////var receiptImage = new Mat();
            ////using (var requestStream = await Request.Content.ReadAsStreamAsync())
            ////{
            ////    Mat.FromStream(requestStream, ImreadModes.Unchanged).CopyTo(receiptImage);
            ////}


            //var requestStream = await Request.Content.ReadAsStreamAsync();
            // ImreadModes.GrayScale or  ImreadModes.Unchanged

            //Mat.FromStream(requestStream, ImreadModes.Unchanged).CopyTo(receiptImage);
            //requestStream.Close();

            ////////______________________________ testing ______________________________
            //////using (new Window("FirstPicture", WindowMode.Normal, receiptImage))
            //////{
            //////}
            ////////_____________________________________________________________________

            //////var receipt = _receiptService.ProcessReceiptImage(receiptImage);


            if (receipt.ItemsList.Count == 0) return HelperClass.JsonHttpResponse<object>(null);

            if (!await isAuthenticated) return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            try
            {
                var receiptId = _itemManager.AddItems(receipt, username);
                return HelperClass.JsonHttpResponse(receiptId);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
    }
}