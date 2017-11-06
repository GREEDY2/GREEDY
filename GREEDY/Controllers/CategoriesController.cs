using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using GREEDY.DataManagers;
using GREEDY.Extensions;

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
            var categories = _categoryManager.GetAllDistinctCategories();
            return HelperClass.JsonHttpResponse(categories);
        }
    }

    /*
     public async Task<HttpResponseMessage> Put()
        {
            HttpContent content = Request.Content;
            string jsonContent = await content.ReadAsStringAsync();
            //TODO: this is totaly unfinished, need to consult with the team
            var temp = JsonConvert.DeserializeObject(jsonContent);
            return HelperClass.JsonHttpResponse<Object>(null);
        }
     */
}
