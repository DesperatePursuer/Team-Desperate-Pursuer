namespace AssetTrackingSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.GeneralCategories", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.SubCategories", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubCategories", "IsDeleted");
            DropColumn("dbo.GeneralCategories", "IsDeleted");
            DropColumn("dbo.Categories", "IsDeleted");
        }
    }
}
