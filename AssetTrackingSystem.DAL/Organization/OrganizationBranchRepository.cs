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
   public class OrganizationBranchRepository : IDisposable
    {
        private AssetTrackingSystemDBContext db;

        public OrganizationBranchRepository()
        {
            db = new AssetTrackingSystemDBContext();
        }


        public List<OrganizationBranch> GetAll()
        {
            var organizationbranch = db.OrganizationBranches.AsQueryable();
            organizationbranch = organizationbranch.Where(c => c.IsDeleted == false);
            return organizationbranch.OrderBy(o => o.Name).ToList();
        }


        public List<Models.Organization> GetOrganizationSelectListItems()
        {
            var organization = db.Organizations.AsQueryable();
            organization = organization.Where(c => c.IsDeleted == false);
            return organization.OrderBy(o => o.Name).ToList();


        }


        //CRUD START

        public int Save(OrganizationBranch organizationBranch)
        {
            db.OrganizationBranches.Add(organizationBranch);
            int rowAffected = db.SaveChanges();

            return rowAffected;
        }

        public int Update(OrganizationBranch organizationBranch)
        {
            db.OrganizationBranches.Attach(organizationBranch);

            db.Entry(organizationBranch).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public List<OrganizationBranch> GetAll_Branch()
        {
            var organizationBranch = db.OrganizationBranches.AsQueryable();
            organizationBranch = organizationBranch.Where(c => c.IsDeleted == false);
            return organizationBranch.OrderBy(o => o.Name).ToList();
        }






        public OrganizationBranch GetByID(int? id)
        {

            var selectedOrganizationBranch = db.OrganizationBranches.SingleOrDefault(c => c.Id == id);

            return selectedOrganizationBranch;

        }


        public List<OrganizationBranch> GetOrganizationBranchBySearchItem(OrganizationBranch organizationBranchVM)
        {
            var organizationBranch = db.OrganizationBranches.AsQueryable();



            if (!string.IsNullOrEmpty(organizationBranchVM.Name))

            {
                organizationBranch =
                    organizationBranch.Where(
                        c =>
                            c.IsDeleted == false &&
                            (c.Name.ToLower().Contains(organizationBranchVM.Name.ToLower())));
            }

            else
            {
                organizationBranch = organizationBranch.Where(c => c.IsDeleted == false);
                return organizationBranch.OrderBy(o => o.Name).ToList();
            }



            return organizationBranch.OrderBy(o => o.Name).ToList();
        }

        public int Delete(int? id)
        {
            int? ID = id;

            OrganizationBranch organizationBranch = db.OrganizationBranches.First(c => c.Id == ID);

            organizationBranch.IsDeleted = true;

            int rowAffected = db.SaveChanges();

            return rowAffected;


        }


        // CRUD END











        public void Dispose()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }


    }
}
