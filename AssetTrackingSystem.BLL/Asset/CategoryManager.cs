using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetTrackingSystem.DAL.Asset;
using AssetTrackingSystem.Model.Models;

namespace AssetTrackingSystem.BLL.Asset
{
    public class CategoryManager
    {

        private CategoryRepository _Repository;

        public CategoryManager()
        {
            _Repository = new CategoryRepository();
        }

        public bool Save(Category obj)
        {
            // save code ...

            int rowAffected = _Repository.Save(obj);
            bool isSaved = rowAffected > 0;
            return isSaved;

        }

        public bool Update(Category obj)
        {
            int rowAffected = _Repository.Update(obj);

            bool isUpdated = rowAffected > 0;
            return isUpdated;
        }

        public List<Category> GetAll()
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



        public Category GetByID(int? id)
        {



            var var = _Repository.GetByID(id);
            return var;
        }

        public List<Category> GetBySearchItem(Category obj)
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
