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
  public  class CategoryRepository : IDisposable
    {

        private AssetTrackingSystemDBContext db;

        public CategoryRepository()
        {
            db = new AssetTrackingSystemDBContext();
        }



        public int Save(Category obj)
        {
            db.Categories.Add(obj);
            int rowAffected = db.SaveChanges();

            return rowAffected;
        }

        public int Update(Category obj)
        {
            db.Categories.Attach(obj);

            db.Entry(obj).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public List<Category> GetAll()
        {
            var var = db.Categories.AsQueryable();
            var = var.Where(c => c.IsDeleted == false);
            return var.OrderBy(o => o.Name).ToList();
        }



        public Category GetByID(int? id)
        {

            var var = db.Categories.SingleOrDefault(c => c.Id == id);

            return var;

        }


        public List<Category> GetBySearchItem(Category obj)
        {
            var dbobj = db.Categories.AsQueryable();



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

            Category obj = db.Categories.Where(c => c.Id == ID).First();

            obj.IsDeleted = true;

            int rowAffected = db.SaveChanges();

            return rowAffected;


        }








        public void Dispose()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }

      public List<GeneralCategory> GetSelectListItems()
      {
            var var = db.GeneralCategories.AsQueryable();
            var = var.Where(c => c.IsDeleted == false);
            return var.OrderBy(o => o.Name).ToList();

        }
    }
}
