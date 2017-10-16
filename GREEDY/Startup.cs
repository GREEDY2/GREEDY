using Owin;
using System.Linq;
using System.Web.Http;

namespace GREEDY
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration CONFIG = new HttpConfiguration();
            CONFIG.EnableCors();
            CONFIG.Formatters.Select(x => x == CONFIG.Formatters.JsonFormatter);
            CONFIG.Routes.MapHttpRoute(
                name: "createUserApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
            appBuilder.UseWebApi(CONFIG);
        }
    }
}
