namespace GREEDY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredForCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ItemDataModels", "Category_CategoryName", "dbo.CategoryDataModels");
            DropIndex("dbo.ItemDataModels", new[] { "Category_CategoryName" });
            AlterColumn("dbo.ItemDataModels", "Category_CategoryName", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ItemDataModels", "Category_CategoryName");
            AddForeignKey("dbo.ItemDataModels", "Category_CategoryName", "dbo.CategoryDataModels", "CategoryName", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemDataModels", "Category_CategoryName", "dbo.CategoryDataModels");
            DropIndex("dbo.ItemDataModels", new[] { "Category_CategoryName" });
            AlterColumn("dbo.ItemDataModels", "Category_CategoryName", c => c.String(maxLength: 128));
            CreateIndex("dbo.ItemDataModels", "Category_CategoryName");
            AddForeignKey("dbo.ItemDataModels", "Category_CategoryName", "dbo.CategoryDataModels", "CategoryName");
        }
    }
}
