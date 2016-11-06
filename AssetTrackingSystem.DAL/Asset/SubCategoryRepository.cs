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
    public class SubCategoryRepository : IDisposable
    {

        private AssetTrackingSystemDBContext db;

        public SubCategoryRepository()
        {
            db = new AssetTrackingSystemDBContext();
        }



        public int Save(SubCategory obj)
        {
            db.SubCategories.Add(obj);
            int rowAffected = db.SaveChanges();

            return rowAffected;
        }

        public int Update(SubCategory obj)
        {
            db.SubCategories.Attach(obj);

            db.Entry(obj).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public List<SubCategory> GetAll()
        {
            var var = db.SubCategories.AsQueryable();
            var = var.Where(c => c.IsDeleted == false);
            return var.OrderBy(o => o.Name).ToList();
        }



        public SubCategory GetByID(int? id)
        {

            var var = db.SubCategories.SingleOrDefault(c => c.Id == id);

            return var;

        }

        public List<SubCategory> GetBySearchItem(SubCategory obj)
        {
            var dbobj = db.SubCategories.AsQueryable();



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

            SubCategory obj = db.SubCategories.Where(c => c.Id == ID).First();

            obj.IsDeleted = true;

            int rowAffected = db.SaveChanges();

            return rowAffected;


        }


        public List<Category> GetCategoriesByGeneralCategory(int generalCategoryId)
        {
            return db.Categories.Where(c => c.GeneralCategoryId == generalCategoryId).ToList();
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
