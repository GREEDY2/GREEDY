using System.Data.Entity;
namespace GREEDY.Data
{
    public class DataBaseModel:DbContext
    {
        public DataBaseModel(string connectionString):base()
        {
            Database.SetInitializer<DataBaseModel>(new DropCreateDatabaseIfModelChanges<DataBaseModel>());
        }
        public DataBaseModel() : base()
        {
            Database.SetInitializer<DataBaseModel>(new DropCreateDatabaseIfModelChanges<DataBaseModel>());
        }

        public DbSet<UserDataModel> User { get; set; }
        public DbSet<ReceiptDataModel> Receipt { get; set; }
        public DbSet<ShopDataModel> Shop { get; set; }
        public DbSet<ItemDataModel> Item { get; set; }
        public DbSet<CategoryDataModel> Category { get; set; }
        public DbSet<LoginSessionDataModel> LoginSession { get; set; }
    }
}
