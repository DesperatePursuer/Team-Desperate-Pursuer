using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AssetTrackingSystem.DAL.Context;
using AssetTrackingSystem.Models;

namespace AssetTrackingSystem.DAL
{
    public class OrganizationRepository:IDisposable
    {
        private AssetTrackingSystemDBContext db;

        public OrganizationRepository()
        {
            db = new AssetTrackingSystemDBContext();
        }

        public int Save(Models.Organization organization)
        {
            db.Organizations.Add(organization);
            int rowAffected = db.SaveChanges();

            return rowAffected;
        }

        public int Update(Models.Organization organization)
        {
            db.Organizations.Attach(organization);

            db.Entry(organization).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public List<Models.Organization> GetAll()
        {
            var organizations = db.Organizations.AsQueryable();
            organizations = organizations.Where(c => c.IsDeleted == false);
            return organizations.OrderBy(o => o.Name).ToList();
        }



        public Models.Organization GetByID(int? id)
        {

            var selectedOrganization = db.Organizations.SingleOrDefault(c => c.Id == id);

            return selectedOrganization;

        }


        public List<Models.Organization> GetOrganizationBySearchItem(Models.Organization organizationSearchVM)
        {
            var organizations = db.Organizations.AsQueryable();



            if (!string.IsNullOrEmpty(organizationSearchVM.Code))

            {
                organizations =
                    organizations.Where(
                        c =>
                            c.IsDeleted == false &&
                            (c.Name.ToLower().Contains(organizationSearchVM.Code.ToLower()) ||
                             c.Code.ToLower().Contains(organizationSearchVM.Code.ToLower())));
            }

            else
            {
                organizations = organizations.Where(c => c.IsDeleted == false);
                return organizations.OrderBy(o => o.Name).ToList();
            }



            return organizations.OrderBy(o => o.Name).ToList();
        }

        public int Delete(int? id)
        {
            int? ID = id;

            Models.Organization organization = db.Organizations.Where(c => c.Id == ID).First();

            organization.IsDeleted = true;

            int rowAffected = db.SaveChanges();

            return rowAffected;


        }

        public void Dispose()
        {
            if (db!=null)
            {
                db.Dispose();
            }
        }


       
    }
}