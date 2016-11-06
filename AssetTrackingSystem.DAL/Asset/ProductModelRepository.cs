using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetTrackingSystem.DAL.Context;
using AssetTrackingSystem.Model.Models;

namespace AssetTrackingSystem.DAL.Asset
{
    public class ProductModelRepository : IDisposable
    {

        private AssetTrackingSystemDBContext db;

        public ProductModelRepository()
        {
            db = new AssetTrackingSystemDBContext();
        }



        public int Save(ProductModel obj)
        {
            db.ProductModels.Add(obj);
            int rowAffected = db.SaveChanges();

            return rowAffected;
        }

        public int Update(ProductModel obj)
        {
            db.ProductModels.Attach(obj);

            db.Entry(obj).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public List<ProductModel> GetAll()
        {
            var var = db.ProductModels.AsQueryable();
            var = var.Where(c => c.IsDeleted == false);
            return var.OrderBy(o => o.Name).ToList();
        }



        public ProductModel GetByID(int? id)
        {

            var var = db.ProductModels.SingleOrDefault(c => c.Id == id);

            return var;

        }

        public List<ProductModel> GetBySearchItem(ProductModel obj)
        {
            var dbobj = db.ProductModels.AsQueryable();



            if (!string.IsNullOrEmpty(obj.Name))

            {
                dbobj =
                    dbobj.Where(
                        c =>
                            c.IsDeleted == false &&
                            (c.Name.ToLower().Contains(obj.Name.ToLower())));
            }

            else
            {
                dbobj = dbobj.Where(c => c.IsDeleted == false);
                return dbobj.OrderBy(o => o.Name).ToList();
            }



            return dbobj.OrderBy(o => o.Name).ToList();
        }


        public int Delete(int? id)
        {
            int? ID = id;

            ProductModel obj = db.ProductModels.Where(c => c.Id == ID).First();

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
