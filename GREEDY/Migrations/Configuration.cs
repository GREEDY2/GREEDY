namespace GREEDY.Migrations
{
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
                new ShopDataModel() {
                    Name ="IKI", Location = new Geocoding.Location(54.678853,25.278207),
                    Address = "Pylimo g. 21, Vilnius", SubName ="Palink"},
                new ShopDataModel() {
                    Name ="IKI", Location = new Geocoding.Location(54.728869,25.269291),
                    Address = "Didlaukio g. 80A, Vilnius", SubName ="Palink"},
                new ShopDataModel() {
                    Name ="IKI", Location = new Geocoding.Location(54.687653,25.275257),
                    Address = "Gedimino pr. 28, Vilnius", SubName ="Palink"},
                new ShopDataModel() {
                    Name ="IKI", Location = new Geocoding.Location(54.670692,25.281783),
                    Address = "Sodu g. 22, Vilnius", SubName ="Palink"},
                new ShopDataModel() {
                    Name ="IKI", Location = new Geocoding.Location(54.673236,25.274738),
                    Address = "Mindaugo g. 25, Vilnius", SubName ="Palink"},
                new ShopDataModel() {
                    Name ="IKI", Location = new Geocoding.Location(54.722358,25.297907),
                    Address = "Zirmunu g. 145, Vilnius", SubName ="Palink"},
                new ShopDataModel() {
                    Name ="IKI", Location = new Geocoding.Location(54.725833,25.299526),
                    Address = "Zirmunu g. 106, Vilnius", SubName ="Palink"},

                new ShopDataModel() {
                    Name ="MAXIMA", Location = new Geocoding.Location(54.678095,25.272716),
                    Address = "Mindaugo g. 11, Vilnius"},
                new ShopDataModel() {
                    Name ="MAXIMA", Location = new Geocoding.Location(54.727977,25.278374),
                    Address = "Didlaukio g. 1, Vilnius"},
                new ShopDataModel() {
                    Name ="MAXIMA", Location = new Geocoding.Location(54.682208,25.255373),
                    Address = "M. K. Ciurlionio g. 82, Vilnius"},
                new ShopDataModel() {
                    Name ="MAXIMA", Location = new Geocoding.Location(54.848567, 25.466849),
                    Address = "Svencioniu g. 44, Nemencine 15174, Lietuva"},

                new ShopDataModel() {
                    Name ="RIMI", Location = new Geocoding.Location(54.708354,25.313375),
                    Address = "Antakalnio g. 55, Vilnius"},
                new ShopDataModel() {
                    Name ="RIMI", Location = new Geocoding.Location(54.678257,25.287651),
                    Address = "Didžioji g. 28, Vilnius"},
                new ShopDataModel() {
                    Name ="RIMI", Location = new Geocoding.Location(54.673904,25.213369),
                    Address = "Architektu g. 19, Vilnius"},
                new ShopDataModel() {
                    Name ="RIMI", Location = new Geocoding.Location(54.687019,25.281393),
                    Address = "Gedimino pr. 9, Vilnius"},

                new ShopDataModel() {
                    Name ="LIDL", Location = new Geocoding.Location(54.672603,25.256474),
                    Address = "Zemaites g. 16"},
                new ShopDataModel() {
                    Name ="LIDL", Location = new Geocoding.Location(54.673994,25.296879),
                    Address = "Rasu g. 9A"},
                new ShopDataModel() {
                    Name ="LIDL", Location = new Geocoding.Location(54.713697,25.28571),
                    Address = "Kalvariju g. 180"},
                new ShopDataModel() {
                    Name ="LIDL", Location = new Geocoding.Location(54.711871,25.240719),
                    Address = "Dukstu g. 34"},
                new ShopDataModel() {
                    Name ="LIDL", Location = new Geocoding.Location(54.693034,25.219452),
                    Address = "Sausio 13-osios g. 3"},
                new ShopDataModel() {
                    Name ="LIDL", Location = new Geocoding.Location(54.721467,25.253155),
                    Address = "Staneviciaus g. 2A"},
                new ShopDataModel() {
                    Name ="LIDL", Location = new Geocoding.Location(54.733398,25.251695),
                    Address = "Ateities g. 4A"},
                new ShopDataModel() {
                    Name ="LIDL", Location = new Geocoding.Location(54.729192,25.216438),
                    Address = "Justiniskiu g. 126"},
                new ShopDataModel() {
                    Name ="LIDL", Location = new Geocoding.Location(54.737923,25.230966),
                    Address = "S. Neries g. 16"},

                new ShopDataModel() {
                    Name ="IKI", SubName ="Palink", Location = new Geocoding.Location(0,0)},
                new ShopDataModel() {
                    Name ="MAXIMA", Location = new Geocoding.Location(0,0)},
                new ShopDataModel() {
                    Name ="RIMI", Location = new Geocoding.Location(0,0)},
                new ShopDataModel() {
                    Name ="LIDL", Location = new Geocoding.Location(0,0)},
                new ShopDataModel() {
                    Name ="NORFA", Location = new Geocoding.Location(0,0)},
            };

            var Categories = new CategoryDataModel[]{
                new CategoryDataModel() {
                CategoryName = "dairy"},
                new CategoryDataModel() {
                CategoryName = "alcoholic drinks"},
                new CategoryDataModel() {
                CategoryName = "fruits"},
                new CategoryDataModel() {
                CategoryName = "vegetables"},
                new CategoryDataModel() {
                CategoryName = "flour products"},
                new CategoryDataModel() {
                CategoryName = "non-alcoholic drinks"},
                new CategoryDataModel() {
                CategoryName = "sweets"},
                new CategoryDataModel() {
                CategoryName = "meat products"},
                new CategoryDataModel() {
                CategoryName = "snacks"},
                new CategoryDataModel() {
                CategoryName = "deposit"},
                new CategoryDataModel() {
                CategoryName = "other"},
                new CategoryDataModel() {
                CategoryName = "discount"},
                new CategoryDataModel() {
                CategoryName = "food"}
            };

            foreach (ShopDataModel shop in Shops)
                if (context.Set<ShopDataModel>().FirstOrDefault(x => x.Name == shop.Name) == null)
                    context.Set<ShopDataModel>().Add(shop);

            foreach (CategoryDataModel categ in Categories)
                if (context.Set<CategoryDataModel>().FirstOrDefault(x => x.CategoryName == categ.CategoryName) == null)
                    context.Set<CategoryDataModel>().Add(categ);

            context.SaveChanges();
        }
    }
}