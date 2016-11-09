using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetTrackingSystem.DAL.Asset;
using AssetTrackingSystem.DAL.Organization;
using AssetTrackingSystem.Model.Models;

namespace AssetTrackingSystem.BLL.Organization
{
    public class AssetLocationManager
    {
        private AssetLocationRepository _Repository;

        public AssetLocationManager()
        {
            _Repository = new AssetLocationRepository();
        }

        public bool Save(AssetLocation obj)
        {
            // save code ...

            int rowAffected = _Repository.Save(obj);
            bool isSaved = rowAffected > 0;
            return isSaved;

        }

        public bool Update(AssetLocation obj)
        {
            int rowAffected = _Repository.Update(obj);

            bool isUpdated = rowAffected > 0;
            return isUpdated;
        }

        public List<AssetLocation> GetAll()
        {
            var var = _Repository.GetAll();

            return var;

        }

        // General AssetLocation Select List
        public List<Models.Organization> GetSelectListItems()
        {
            var var = _Repository.GetSelectListItems();

            return var;

        }


        // General AssetLocation Select List



        public AssetLocation GetByID(int? id)
        {



            var var = _Repository.GetByID(id);
            return var;
        }

        public List<AssetLocation> GetBySearchItem(AssetLocation obj)
        {
            var var = _Repository.GetBySearchItem(obj);
            return var;
        }

        public List<OrganizationBranch> GetOrganizationBranchByOrganization(int organizationId)
        {
            return _Repository.GetOrganizationBranchByOrganization(organizationId);
        }

        public bool Delete(int? id)
        {
            int rowAffected = _Repository.Delete(id);

            bool isDeleted = rowAffected > 0;

            return isDeleted;

        }
    }
}
