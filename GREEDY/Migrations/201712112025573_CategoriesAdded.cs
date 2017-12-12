namespace GREEDY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoriesAdded : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CategoryDataModels");
            AddColumn("dbo.CategoryDataModels", "CategoryName", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.ItemDataModels", "Category_CategoryName", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.CategoryDataModels", "CategoryName");
            CreateIndex("dbo.ItemDataModels", "Category_CategoryName");
            AddForeignKey("dbo.ItemDataModels", "Category_CategoryName", "dbo.CategoryDataModels", "CategoryName");
            DropColumn("dbo.CategoryDataModels", "Keyword");
            DropColumn("dbo.CategoryDataModels", "Category");
            DropColumn("dbo.ItemDataModels", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemDataModels", "Category", c => c.String());
            AddColumn("dbo.CategoryDataModels", "Category", c => c.String(nullable: false));
            AddColumn("dbo.CategoryDataModels", "Keyword", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.ItemDataModels", "Category_CategoryName", "dbo.CategoryDataModels");
            DropIndex("dbo.ItemDataModels", new[] { "Category_CategoryName" });
            DropPrimaryKey("dbo.CategoryDataModels");
            DropColumn("dbo.ItemDataModels", "Category_CategoryName");
            DropColumn("dbo.CategoryDataModels", "CategoryName");
            AddPrimaryKey("dbo.CategoryDataModels", "Keyword");
        }
    }
}
