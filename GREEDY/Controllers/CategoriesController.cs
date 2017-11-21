using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using GREEDY.DataManagers;
using GREEDY.Extensions;
using System;
using GREEDY.Services;
using System.Threading.Tasks;

namespace GREEDY.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GetDistinctCategoriesController : ApiController
    {
        private ICategoryManager _categoryManager;
        private IAuthenticationService _authenticationService;
        public GetDistinctCategoriesController(ICategoryManager categoryManager, IAuthenticationService authenticationService)
        {
            _categoryManager = categoryManager;
            _authenticationService = authenticationService;
        }
        public async Task<HttpResponseMessage> Get()
        {
            Request.RegisterForDispose((IDisposable)_categoryManager);
            var token = Request.Headers.Authorization.Parameter;
            var isAuthenticated = _authenticationService.ValidateToken(token);
            if (await isAuthenticated)
            {
                var categories = _categoryManager.GetAllDistinctCategories();
                return HelperClass.JsonHttpResponse(categories);
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }
}
