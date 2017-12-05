namespace GREEDY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Shop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShopDataModels", "Location_Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.ShopDataModels", "Location_Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.ShopDataModels", "Address", c => c.String());
            DropColumn("dbo.ShopDataModels", "Location");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShopDataModels", "Location", c => c.String());
            DropColumn("dbo.ShopDataModels", "Address");
            DropColumn("dbo.ShopDataModels", "Location_Longitude");
            DropColumn("dbo.ShopDataModels", "Location_Latitude");
        }
    }
}
