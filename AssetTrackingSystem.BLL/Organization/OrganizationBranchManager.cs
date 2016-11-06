using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetTrackingSystem.DAL;
using AssetTrackingSystem.DAL.Organization;
using AssetTrackingSystem.Model.Models;

namespace AssetTrackingSystem.BLL.Organization
{
   public class OrganizationBranchManager
    {

        private OrganizationBranchRepository _organizationBranchRepository;

        public OrganizationBranchManager()
        {
            _organizationBranchRepository = new OrganizationBranchRepository();
        }




        public List<OrganizationBranch> GetAll()
        {
            var allOrganization = _organizationBranchRepository.GetAll();

            return allOrganization;

        }

        public List<Models.Organization> GetOrganizationSelectListItems()
        {
            var allOrganization = _organizationBranchRepository.GetOrganizationSelectListItems();

            return allOrganization;

        }


        public bool Save(OrganizationBranch organizationBranch)
        {
            // save code ...

            int rowAffected = _organizationBranchRepository.Save(organizationBranch);
            bool isSaved = rowAffected > 0;
            return isSaved;

        }

        public bool Update(OrganizationBranch organizationBranch)
        {
            int rowAffected = _organizationBranchRepository.Update(organizationBranch);

            bool isUpdated = rowAffected > 0;
            return isUpdated;
        }

        public OrganizationBranch GetByID(int? id)
        {



            var selectedOrganization = _organizationBranchRepository.GetByID(id);
            return selectedOrganization;
        }

        public List<OrganizationBranch> GetOrganizationBySearchItem(OrganizationBranch oraganization)
        {
            var organizaitons = _organizationBranchRepository.GetOrganizationBranchBySearchItem(oraganization);
            return organizaitons;
        }

        public bool Delete(int? id)
        {
            int rowAffected = _organizationBranchRepository.Delete(id);

            bool isDeleted = rowAffected > 0;

            return isDeleted;

        }





    }
}
