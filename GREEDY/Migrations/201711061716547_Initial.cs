namespace GREEDY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryDataModels",
                c => new
                    {
                        Keyword = c.String(nullable: false, maxLength: 128),
                        Category = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Keyword);
            
            CreateTable(
                "dbo.ItemDataModels",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(nullable: false),
                        Receipt_ReceiptId = c.Int(),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.ReceiptDataModels", t => t.Receipt_ReceiptId)
                .Index(t => t.Receipt_ReceiptId);
            
            CreateTable(
                "dbo.ReceiptDataModels",
                c => new
                    {
                        ReceiptId = c.Int(nullable: false, identity: true),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        Shop_ShopId = c.Int(nullable: false),
                        User_Username = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ReceiptId)
                .ForeignKey("dbo.ShopDataModels", t => t.Shop_ShopId, cascadeDelete: true)
                .ForeignKey("dbo.UserDataModels", t => t.User_Username, cascadeDelete: true)
                .Index(t => t.Shop_ShopId)
                .Index(t => t.User_Username);
            
            CreateTable(
                "dbo.ShopDataModels",
                c => new
                    {
                        ShopId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Location = c.String(),
                        SubName = c.String(),
                    })
                .PrimaryKey(t => t.ShopId);
            
            CreateTable(
                "dbo.UserDataModels",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 255),
                        Email = c.String(nullable: false, maxLength: 255),
                        FullName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Username)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.LoginSessionDataModels",
                c => new
                    {
                        SessionID = c.Guid(nullable: false),
                        User_Username = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.SessionID)
                .ForeignKey("dbo.UserDataModels", t => t.User_Username, cascadeDelete: true)
                .Index(t => t.User_Username);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoginSessionDataModels", "User_Username", "dbo.UserDataModels");
            DropForeignKey("dbo.ReceiptDataModels", "User_Username", "dbo.UserDataModels");
            DropForeignKey("dbo.ReceiptDataModels", "Shop_ShopId", "dbo.ShopDataModels");
            DropForeignKey("dbo.ItemDataModels", "Receipt_ReceiptId", "dbo.ReceiptDataModels");
            DropIndex("dbo.LoginSessionDataModels", new[] { "User_Username" });
            DropIndex("dbo.UserDataModels", new[] { "Email" });
            DropIndex("dbo.ReceiptDataModels", new[] { "User_Username" });
            DropIndex("dbo.ReceiptDataModels", new[] { "Shop_ShopId" });
            DropIndex("dbo.ItemDataModels", new[] { "Receipt_ReceiptId" });
            DropTable("dbo.LoginSessionDataModels");
            DropTable("dbo.UserDataModels");
            DropTable("dbo.ShopDataModels");
            DropTable("dbo.ReceiptDataModels");
            DropTable("dbo.ItemDataModels");
            DropTable("dbo.CategoryDataModels");
        }
    }
}
