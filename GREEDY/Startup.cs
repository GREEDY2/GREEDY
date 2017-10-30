using Owin;
using System.Linq;
using System.Web.Http;
using GREEDY.DataManagers;
using GREEDY.Services;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using System.Reflection;
using Ninject;

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
            appBuilder.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(CONFIG);
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Bind<IItemManager>().To<ItemManager>();
            kernel.Bind<IUserManager>().To<UserManager>();
            kernel.Bind<ICategoryManager>().To<CategoryManager>();
            kernel.Bind<IAuthService>().To<AuthService>();
            return kernel;
        }
    }
}
