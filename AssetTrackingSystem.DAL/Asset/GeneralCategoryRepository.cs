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
   public class GeneralCategoryRepository : IDisposable
    {

        private AssetTrackingSystemDBContext db;

        public GeneralCategoryRepository()
        {
            db = new AssetTrackingSystemDBContext();
        }



        public int Save(GeneralCategory obj)
        {
            db.GeneralCategories.Add(obj);
            int rowAffected = db.SaveChanges();

            return rowAffected;
        }

        public int Update(GeneralCategory obj)
        {
            db.GeneralCategories.Attach(obj);

            db.Entry(obj).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public List<GeneralCategory> GetAll()
        {
            var var = db.GeneralCategories.AsQueryable();
            var = var.Where(c => c.IsDeleted == false);
            return var.OrderBy(o => o.Name).ToList();
        }



        public GeneralCategory GetByID(int? id)
        {

            var var = db.GeneralCategories.SingleOrDefault(c => c.Id == id);

            return var;

        }

        public List<GeneralCategory> GetBySearchItem(GeneralCategory obj)
        {
            var dbobj = db.GeneralCategories.AsQueryable();



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

            GeneralCategory obj = db.GeneralCategories.Where(c => c.Id == ID).First();

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


    }
}
