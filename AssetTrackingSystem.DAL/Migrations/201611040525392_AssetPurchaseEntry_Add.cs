namespace AssetTrackingSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssetPurchaseEntry_Add : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssetPurchaseEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchaseDate = c.String(),
                        Vendor = c.String(),
                        ProductModelId = c.String(),
                        WarrantyPeriod = c.String(),
                        Price = c.Double(nullable: false),
                        Qty = c.Int(nullable: false),
                        SerialNo = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        ProductModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductModels", t => t.ProductModel_Id)
                .Index(t => t.ProductModel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssetPurchaseEntries", "ProductModel_Id", "dbo.ProductModels");
            DropIndex("dbo.AssetPurchaseEntries", new[] { "ProductModel_Id" });
            DropTable("dbo.AssetPurchaseEntries");
        }
    }
}
