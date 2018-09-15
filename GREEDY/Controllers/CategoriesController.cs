using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using GREEDY.DataManagers;
using GREEDY.Extensions;
using GREEDY.Services;

namespace GREEDY.Controllers
{
    [EnableCors("*", "*", "*")]
    public class GetDistinctCategoriesController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ICategoryManager _categoryManager;

        public GetDistinctCategoriesController(ICategoryManager categoryManager,
            IAuthenticationService authenticationService)
        {
            _categoryManager = categoryManager;
            _authenticationService = authenticationService;
        }

        public async Task<HttpResponseMessage> Get()
        {
            Request.RegisterForDispose((IDisposable) _categoryManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token);
            if (await isAuthenticated)
            {
                var categories = _categoryManager.GetAllDistinctCategories();
                return HelperClass.JsonHttpResponse(categories);
            }

            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
    }
}