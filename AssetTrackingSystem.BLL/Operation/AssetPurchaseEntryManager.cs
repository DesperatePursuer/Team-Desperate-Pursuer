using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetTrackingSystem.DAL.Operation;
using AssetTrackingSystem.Model.Models;

namespace AssetTrackingSystem.BLL.Operation
{
    public class AssetPurchaseEntryManager
    {
        private AssetPurchaseEntryRepository _Repository;

        public AssetPurchaseEntryManager()
        {
            _Repository = new AssetPurchaseEntryRepository();
        }

        public bool Save(AssetPurchaseEntry obj)
        {
            // save code ...

            int rowAffected = _Repository.Save(obj);
            bool isSaved = rowAffected > 0;
            return isSaved;

        }

        public bool Update(AssetPurchaseEntry obj)
        {
            int rowAffected = _Repository.Update(obj);

            bool isUpdated = rowAffected > 0;
            return isUpdated;
        }

        public List<AssetPurchaseEntry> GetAll()
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



        public AssetPurchaseEntry GetByID(int? id)
        {



            var var = _Repository.GetByID(id);
            return var;
        }

        public List<AssetPurchaseEntry> GetBySearchItem(AssetPurchaseEntry obj)
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
        public List<SubCategory> GetSubCategoryByCategory(int categoryId)
        {
            return _Repository.GetSubCategoryByCategory(categoryId);
        }
        public List<ProductModel> GetProductModelBySubCategory(int subCategoryId)
        {
            return _Repository.GetProductModelBySubCategory(subCategoryId);
        }
    }
}
