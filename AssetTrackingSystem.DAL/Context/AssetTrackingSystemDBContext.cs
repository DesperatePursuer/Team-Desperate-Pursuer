using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AssetTrackingSystem.Model.Models;


namespace AssetTrackingSystem.DAL.Context
{
    public class AssetTrackingSystemDBContext: DbContext
    {
        public DbSet<Models.Organization> Organizations { get; set; }
        public DbSet<OrganizationBranch> OrganizationBranches { get; set; }

        public DbSet<AssetLocation> AssetLocations { get; set; }



        public DbSet<GeneralCategory> GeneralCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }       
        public DbSet<ProductModel> ProductModels { get; set; }


        public DbSet<AssetPurchaseEntry> AssetPurchaseEntries { get; set; }



    }
}