using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using GREEDY.Data;
using GREEDY.DataManagers;
using GREEDY.ImagePreparation;
using GREEDY.OCRs;
using GREEDY.ReceiptCreating;
using GREEDY.Services;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

namespace GREEDY
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.EnableCors();
            config.Formatters.Select(x => x == config.Formatters.JsonFormatter);
            config.Routes.MapHttpRoute(
                "createUserApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
            );
            config.MessageHandlers.Add(new LogHandler());

            appBuilder.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(config);
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Bind<IItemManager>().To<ItemManager>();
            kernel.Bind<IUserManager>().To<UserManager>();
            kernel.Bind<IShopManager>().To<ShopManager>();
            kernel.Bind<IGraphManager>().To<GraphManager>();
            kernel.Bind<IReceiptManager>().To<ReceiptManager>();
            kernel.Bind<IItemService>().To<ItemService>();
            kernel.Bind<IReceiptMaking>().To<ReceiptMaking>();
            kernel.Bind<IShopDetection>().To<ShopDetection>();
            kernel.Bind<ICategoryManager>().To<CategoryManager>();
            kernel.Bind<IReceiptService>().To<ReceiptService>();
            kernel.Bind<IAuthenticationService>().To<AuthenticationService>();
            kernel.Bind<IImageFormating>().To<ImageFormating>();
            kernel.Bind<IItemCategorization>().To<ItemCategorization>();
            kernel.Bind<IOcr>().To<EmguOcr>();
            kernel.Bind<IDataConverter>().To<DataConverter>();
            kernel.Bind<DbContext>().To<DataBaseModel>();
            return kernel;
        }
    }
}