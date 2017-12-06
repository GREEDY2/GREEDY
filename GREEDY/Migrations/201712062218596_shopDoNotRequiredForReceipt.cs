namespace GREEDY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shopDoNotRequiredForReceipt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReceiptDataModels", "Shop_ShopId", "dbo.ShopDataModels");
            DropIndex("dbo.ReceiptDataModels", new[] { "Shop_ShopId" });
            AlterColumn("dbo.ReceiptDataModels", "Shop_ShopId", c => c.Int());
            CreateIndex("dbo.ReceiptDataModels", "Shop_ShopId");
            AddForeignKey("dbo.ReceiptDataModels", "Shop_ShopId", "dbo.ShopDataModels", "ShopId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReceiptDataModels", "Shop_ShopId", "dbo.ShopDataModels");
            DropIndex("dbo.ReceiptDataModels", new[] { "Shop_ShopId" });
            AlterColumn("dbo.ReceiptDataModels", "Shop_ShopId", c => c.Int(nullable: false));
            CreateIndex("dbo.ReceiptDataModels", "Shop_ShopId");
            AddForeignKey("dbo.ReceiptDataModels", "Shop_ShopId", "dbo.ShopDataModels", "ShopId", cascadeDelete: true);
        }
    }
}
