using System.Data.Entity;
namespace GREEDY.Data
{
    public class DataBaseModel : DbContext
    {
        public DataBaseModel(string connectionString) : base()
        {
            Database.SetInitializer<DataBaseModel>(new DropCreateDatabaseIfModelChanges<DataBaseModel>());
        }
        public DataBaseModel() : base()
        {
            Database.SetInitializer<DataBaseModel>(new DropCreateDatabaseIfModelChanges<DataBaseModel>());
        }

        public virtual DbSet<UserDataModel> User { get; set; }
        public virtual DbSet<ReceiptDataModel> Receipt { get; set; }
        public virtual DbSet<ShopDataModel> Shop { get; set; }
        public virtual DbSet<ItemDataModel> Item { get; set; }
        public virtual DbSet<CategoryDataModel> Category { get; set; }
        public virtual DbSet<LoginSessionDataModel> LoginSession { get; set; }
    }
}
