namespace GREEDY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateSplitting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReceiptDataModels", "ReceiptDate", c => c.DateTime(storeType: "date"));
            AddColumn("dbo.ReceiptDataModels", "UpdateDate", c => c.DateTime(nullable: false, storeType: "date"));
            DropColumn("dbo.ReceiptDataModels", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReceiptDataModels", "Date", c => c.DateTime(nullable: false, storeType: "date"));
            DropColumn("dbo.ReceiptDataModels", "UpdateDate");
            DropColumn("dbo.ReceiptDataModels", "ReceiptDate");
        }
    }
}
