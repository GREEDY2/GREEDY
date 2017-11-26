using Owin;
using System.Linq;
using System.Web.Http;
using GREEDY.DataManagers;
using GREEDY.Services;
using GREEDY.OCRs;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using System.Reflection;
using GREEDY.ImagePreparation;
using Ninject;
using GREEDY.ReceiptCreatings;

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
            CONFIG.MessageHandlers.Add(new LogHandler());

            appBuilder.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(CONFIG);
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Bind<IItemManager>().To<ItemManager>();
            kernel.Bind<IUserManager>().To<UserManager>();
            kernel.Bind<IShopManager>().To<ShopManager>();
            kernel.Bind<IGraphManager>().To<GraphManager>();
            kernel.Bind<IItemService>().To<ItemService>();
            kernel.Bind<IReceiptCreatings>().To<ReceiptCreating>();
            kernel.Bind<ICategoryManager>().To<CategoryManager>();
            kernel.Bind<IReceiptService>().To<ReceiptService>();
            kernel.Bind<IAuthenticationService>().To<AuthenticationService>();
            kernel.Bind<IImageFormating>().To<ImageFormating>();
            kernel.Bind<IItemCategorization>().To<ItemCategorization>();
            kernel.Bind<IOcr>().To<EmguOcr>();
            kernel.Bind<IDataConverter>().To<DataConverter>();
            kernel.Bind<System.Data.Entity.DbContext>().To<Data.DataBaseModel>();
            return kernel;
        }
    }
}
