using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetTrackingSystem.DAL.Asset;
using AssetTrackingSystem.Model.Models;

namespace AssetTrackingSystem.BLL.Asset
{
   public class SubCategoryManager
    {
        private SubCategoryRepository _Repository;

        public SubCategoryManager()
        {
            _Repository = new SubCategoryRepository();
        }

        public bool Save(SubCategory obj)
        {
            // save code ...

            int rowAffected = _Repository.Save(obj);
            bool isSaved = rowAffected > 0;
            return isSaved;

        }

        public bool Update(SubCategory obj)
        {
            int rowAffected = _Repository.Update(obj);

            bool isUpdated = rowAffected > 0;
            return isUpdated;
        }

        public List<SubCategory> GetAll()
        {
            var var = _Repository.GetAll();

            return var;

        }

        // General Category Select List
        public List<GeneralCategory> GetSelectListItems()
        {
            var var = _Repository.GetSelectListItems();

            return var;

        }


        // General Category Select List



        public SubCategory GetByID(int? id)
        {



            var var = _Repository.GetByID(id);
            return var;
        }

        public List<SubCategory> GetBySearchItem(SubCategory obj)
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


        public List<Category> GetCategoriesByGeneralCategory(int generalCategoryId)
        {
            return _Repository.GetCategoriesByGeneralCategory(generalCategoryId);
        }
    }
}
