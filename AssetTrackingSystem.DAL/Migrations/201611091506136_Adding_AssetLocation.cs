namespace AssetTrackingSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_AssetLocation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssetLocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ShortName = c.String(nullable: false),
                        OrganizationBranchId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrganizationBranches", t => t.OrganizationBranchId, cascadeDelete: true)
                .Index(t => t.OrganizationBranchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssetLocations", "OrganizationBranchId", "dbo.OrganizationBranches");
            DropIndex("dbo.AssetLocations", new[] { "OrganizationBranchId" });
            DropTable("dbo.AssetLocations");
        }
    }
}
