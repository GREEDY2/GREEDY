namespace GREEDY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedSessions : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LoginSessionDataModels", "User_Username", "dbo.UserDataModels");
            DropIndex("dbo.LoginSessionDataModels", new[] { "User_Username" });
            DropTable("dbo.LoginSessionDataModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LoginSessionDataModels",
                c => new
                    {
                        SessionID = c.Guid(nullable: false),
                        User_Username = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.SessionID);
            
            CreateIndex("dbo.LoginSessionDataModels", "User_Username");
            AddForeignKey("dbo.LoginSessionDataModels", "User_Username", "dbo.UserDataModels", "Username", cascadeDelete: true);
        }
    }
}
