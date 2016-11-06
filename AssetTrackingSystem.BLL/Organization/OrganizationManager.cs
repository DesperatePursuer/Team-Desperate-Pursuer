using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssetTrackingSystem.DAL;
using AssetTrackingSystem.Model.Models;


namespace AssetTrackingSystem.BLL
{
    public class OrganizationManager
    {
        private OrganizationRepository _organizationRepository;

        public OrganizationManager()
        {
            _organizationRepository = new OrganizationRepository();
        }

        public bool Save(Models.Organization organization)
        {
            // save code ...

            int rowAffected = _organizationRepository.Save(organization);
            bool isSaved = rowAffected > 0;
            return isSaved;

        }

        public bool Update(Models.Organization organization)
        {
            int rowAffected = _organizationRepository.Update(organization);

            bool isUpdated = rowAffected > 0;
            return isUpdated;
        }

        public List<Models.Organization> GetAll()
        {
            var allOrganization = _organizationRepository.GetAll();

            return allOrganization;

        }

        public Models.Organization GetByID(int? id)
        {
          


            var selectedOrganization = _organizationRepository.GetByID(id);
            return selectedOrganization;
        }

        public List<Models.Organization> GetOrganizationBySearchItem(Models.Organization oraganization)
        {
            var organizaitons = _organizationRepository.GetOrganizationBySearchItem(oraganization);
            return organizaitons;
        }

        public bool Delete(int? id)
        {
            int rowAffected = _organizationRepository.Delete(id);

          bool isDeleted = rowAffected>0;

            return isDeleted;

        }
    }
}