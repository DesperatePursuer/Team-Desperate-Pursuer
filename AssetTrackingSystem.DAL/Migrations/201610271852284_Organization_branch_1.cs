namespace AssetTrackingSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Organization_branch_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrganizationBranches", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrganizationBranches", "IsDeleted");
        }
    }
}
