using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using GREEDY.Extensions;
using GREEDY.Models;
using GREEDY.Services;
using Newtonsoft.Json;

namespace GREEDY.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CartController : ApiController
    {
        private readonly ICartService _cartService;
        private readonly IAuthenticationService _authenticationService;

        public CartController(ICartService cartService, IAuthenticationService authenticationService)
        {
            _cartService = cartService;
            _authenticationService = authenticationService;
        }

        public async Task<HttpResponseMessage> Put()
        {
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token, out string username);
            HttpContent content = Request.Content;
            string jsonContent = await content.ReadAsStringAsync();
            var parsedJson = JsonConvert.DeserializeObject<Cart>(jsonContent);
            if (await isAuthenticated)
            {
                var cart = _cartService.UpdateCart(parsedJson, username);
                return HelperClass.JsonHttpResponse(cart);
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }

        public async Task<HttpResponseMessage> Get()
        {
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token, out string username);
            if (await isAuthenticated)
            {
                var cart = _cartService.GetCart(username);
                return HelperClass.JsonHttpResponse(cart);
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }

    }
}
