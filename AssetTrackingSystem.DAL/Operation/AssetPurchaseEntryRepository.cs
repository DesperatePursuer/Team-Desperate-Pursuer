using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetTrackingSystem.DAL.Context;
using AssetTrackingSystem.Model.Models;

namespace AssetTrackingSystem.DAL.Operation
{
    public class AssetPurchaseEntryRepository : IDisposable
    {
        private AssetTrackingSystemDBContext db;

        public AssetPurchaseEntryRepository()
        {
            db = new AssetTrackingSystemDBContext();
        }



        public int Save(AssetPurchaseEntry obj)
        {
            db.AssetPurchaseEntries.Add(obj);
            int rowAffected = db.SaveChanges();

            return rowAffected;
        }

        public int Update(AssetPurchaseEntry obj)
        {
            db.AssetPurchaseEntries.Attach(obj);

            db.Entry(obj).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public List<AssetPurchaseEntry> GetAll()
        {
            var var = db.AssetPurchaseEntries.AsQueryable();
            var = var.Where(c => c.IsDeleted == false);
            return var.OrderBy(o => o.PurchaseDate).ToList();
        }



        public AssetPurchaseEntry GetByID(int? id)
        {

            var var = db.AssetPurchaseEntries.SingleOrDefault(c => c.Id == id);

            return var;

        }

        public List<AssetPurchaseEntry> GetBySearchItem(AssetPurchaseEntry obj)
        {
            var dbobj = db.AssetPurchaseEntries.AsQueryable();



            if (!string.IsNullOrEmpty(obj.PurchaseDate))

            {
                dbobj =
                    dbobj.Where(
                        c =>
                            c.IsDeleted == false &&
                            (c.PurchaseDate.ToLower().Contains(obj.PurchaseDate.ToLower())));
            }

            else
            {
                dbobj = dbobj.Where(c => c.IsDeleted == false);
                return dbobj.OrderBy(o => o.PurchaseDate).ToList();
            }



            return dbobj.OrderBy(o => o.PurchaseDate).ToList();
        }


        public int Delete(int? id)
        {
            int? ID = id;

            AssetPurchaseEntry obj = db.AssetPurchaseEntries.Where(c => c.Id == ID).First();

            obj.IsDeleted = true;

            int rowAffected = db.SaveChanges();

            return rowAffected;


        }


        public List<Category> GetCategoriesByGeneralCategory(int generalCategoryId)
        {
            return db.Categories.Where(c => c.GeneralCategoryId == generalCategoryId).ToList();
        }
        public List<SubCategory> GetSubCategoryByCategory(int categoryId)
        {
            return db.SubCategories.Where(c => c.CategoryId == categoryId).ToList();
        }
        public List<ProductModel> GetProductModelBySubCategory(int subCategoryId)
        {
            return db.ProductModels.Where(c => c.SubCategoryId == subCategoryId).ToList();
        }
        public List<GeneralCategory> GetSelectListItems()
        {
            var var = db.GeneralCategories.AsQueryable();
            var = var.Where(c => c.IsDeleted == false);
            return var.OrderBy(o => o.Name).ToList();
        }





        public void Dispose()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }


    }
}
