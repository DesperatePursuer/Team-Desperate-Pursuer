using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetTrackingSystem.DAL.Context;
using AssetTrackingSystem.Model.Models;

namespace AssetTrackingSystem.DAL.Organization
{
    public class AssetLocationRepository
    {
        private AssetTrackingSystemDBContext db;

        public AssetLocationRepository()
        {
            db = new AssetTrackingSystemDBContext();
        }



        public int Save(AssetLocation obj)
        {
            db.AssetLocations.Add(obj);
            int rowAffected = db.SaveChanges();

            return rowAffected;
        }

        public int Update(AssetLocation obj)
        {
            db.AssetLocations.Attach(obj);

            db.Entry(obj).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public List<AssetLocation> GetAll()
        {
            var var = db.AssetLocations.AsQueryable();
            var = var.Where(c => c.IsDeleted == false);
            return var.OrderBy(o => o.Name).ToList();
        }



        public AssetLocation GetByID(int? id)
        {

            var var = db.AssetLocations.SingleOrDefault(c => c.Id == id);

            return var;

        }

        public List<AssetLocation> GetBySearchItem(AssetLocation obj)
        {
            var dbobj = db.AssetLocations.AsQueryable();



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

            AssetLocation obj = db.AssetLocations.Where(c => c.Id == ID).First();

            obj.IsDeleted = true;

            int rowAffected = db.SaveChanges();

            return rowAffected;


        }


        public List<OrganizationBranch> GetOrganizationBranchByOrganization(int organizationId)
        {
            return db.OrganizationBranches.Where(c => c.OrganizationId == organizationId).ToList();
        }
        public List<Models.Organization> GetSelectListItems()
        {
            var var = db.Organizations.AsQueryable();
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
