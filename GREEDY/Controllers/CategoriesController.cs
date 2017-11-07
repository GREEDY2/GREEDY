using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using GREEDY.DataManagers;
using GREEDY.Extensions;
using System;

namespace GREEDY.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GetDistinctCategoriesController : ApiController
    {
        private ICategoryManager _categoryManager;

        public GetDistinctCategoriesController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }
        public HttpResponseMessage Get()
        {
            Request.RegisterForDispose((IDisposable)_categoryManager);
            var categories = _categoryManager.GetAllDistinctCategories();
            return HelperClass.JsonHttpResponse(categories);
        }
    }
}
