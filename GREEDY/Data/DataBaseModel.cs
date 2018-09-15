using System.Data.Entity;

namespace GREEDY.Data
{
    public class DataBaseModel : DbContext
    {
        public virtual DbSet<UserDataModel> User { get; set; }
        public virtual DbSet<ReceiptDataModel> Receipt { get; set; }
        public virtual DbSet<ShopDataModel> Shop { get; set; }
        public virtual DbSet<ItemDataModel> Item { get; set; }
        public virtual DbSet<CategoryDataModel> Category { get; set; }
    }
}