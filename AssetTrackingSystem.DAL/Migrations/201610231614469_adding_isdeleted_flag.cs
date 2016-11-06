namespace AssetTrackingSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adding_isdeleted_flag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organizations", "IsDeleted");
        }
    }
}
