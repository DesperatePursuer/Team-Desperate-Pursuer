using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetTrackingSystem.DAL.Asset;
using AssetTrackingSystem.Model.Models;

namespace AssetTrackingSystem.BLL.Asset
{
   public class ProductModelManager
    {
        private ProductModelRepository _Repository;

        public ProductModelManager()
        {
            _Repository = new ProductModelRepository();
        }

        public bool Save(ProductModel obj)
        {
            // save code ...

            int rowAffected = _Repository.Save(obj);
            bool isSaved = rowAffected > 0;
            return isSaved;

        }

        public bool Update(ProductModel obj)
        {
            int rowAffected = _Repository.Update(obj);

            bool isUpdated = rowAffected > 0;
            return isUpdated;
        }

        public List<ProductModel> GetAll()
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



        public ProductModel GetByID(int? id)
        {



            var var = _Repository.GetByID(id);
            return var;
        }

        public List<ProductModel> GetBySearchItem(ProductModel obj)
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
    }
}
