namespace GREEDY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FacebookLogin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDataModels", "IsFacebookUser", c => c.Boolean(nullable: false));
            AlterColumn("dbo.UserDataModels", "Password", c => c.String());
            Sql("ALTER TABLE UserDataModels ADD CONSTRAINT CK_Password_IsFacebookUser CHECK ((IsFacebookUser=1 AND PASSWORD IS NULL) OR (IsFacebookUser=0 AND PASSWORD IS NOT NULL))");
            Sql("UPDATE UserDataModels SET IsFacebookUser = 0 WHERE Password IS NOT NULL");
            Sql("UPDATE UserDataModels SET IsFacebookUser = 1 WHERE Password IS NULL");
        }

        public override void Down()
        {
            AlterColumn("dbo.UserDataModels", "Password", c => c.String(nullable: false));
            DropColumn("dbo.UserDataModels", "IsFacebookUser");
        }
    }
}
