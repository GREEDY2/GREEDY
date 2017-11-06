namespace GREEDY.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using GREEDY.Data;

    internal sealed class Configuration : DbMigrationsConfiguration<GREEDY.Data.DataBaseModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GREEDY.Data.DataBaseModel context)
        {

            //  This method will be called after migrating to the latest version.
            var Shops = new ShopDataModel[]{
                new ShopDataModel() { Name="IKI",SubName="Palink"},
                new ShopDataModel() { Name = "MAXIMA"},
                new ShopDataModel() { Name = "RIMI" },
                new ShopDataModel() { Name = "LIDL" }
            };

            foreach (ShopDataModel shop in Shops)
                if (context.Set<ShopDataModel>().FirstOrDefault(x => x.Name == shop.Name) == null)
                    context.Set<ShopDataModel>().Add(shop);
            context.SaveChanges();

        }
    }
}
