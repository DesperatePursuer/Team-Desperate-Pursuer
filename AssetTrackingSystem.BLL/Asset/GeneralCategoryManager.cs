using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetTrackingSystem.DAL.Asset;
using AssetTrackingSystem.Model.Models;

namespace AssetTrackingSystem.BLL.Asset
{
    public class GeneralCategoryManager
    {
        private GeneralCategoryRepository _Repository;

        public GeneralCategoryManager()
        {
            _Repository = new GeneralCategoryRepository();
        }

        public bool Save(GeneralCategory obj)
        {
            // save code ...

            int rowAffected = _Repository.Save(obj);
            bool isSaved = rowAffected > 0;
            return isSaved;

        }

        public bool Update(GeneralCategory obj)
        {
            int rowAffected = _Repository.Update(obj);

            bool isUpdated = rowAffected > 0;
            return isUpdated;
        }

        public List<GeneralCategory> GetAll()
        {
            var var = _Repository.GetAll();

            return var;

        }

        public GeneralCategory GetByID(int? id)
        {



            var var = _Repository.GetByID(id);
            return var;
        }

        public List<GeneralCategory> GetBySearchItem(GeneralCategory obj)
        {
            var var = _Repository.GetBySearchItem(obj);
            return var;
        }

        public bool Delete(int? id)
        {
            int rowAffected = _Repository.Delete(id);

            bool isDeleted = rowAffected > 0;

            return isDeleted;

        }


    }
}
