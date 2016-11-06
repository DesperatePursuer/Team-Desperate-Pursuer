using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssetTrackingSystem.BLL.Asset;
using AssetTrackingSystem.Model.Models;
using AssetTrackingSystem.Model.Models.ViewModels;

namespace AssetTrackingSystem.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        private CategoryManager _Manager;

        public CategoryController()
        {
            _Manager = new CategoryManager();
        }




        string message = "";
        bool status = false;




        [HttpPost]

        public JsonResult Create(CategoryCreateVM vmObj)
        {


            bool isSaved = false;

            if (ModelState.IsValid)
            {
                var var = vmObj;

                isSaved = _Manager.Save(var.Category);
            }



            if (isSaved)
            {
                status = true;
                message = "Succesfully Saved";
            }
            else
            {
                message = "Error! Please try again.";
            }

            return new JsonResult { Data = new { status = status, message = message } };
        }


        public ActionResult Index()
        {
            var selectListItem = GetSelectListItems();
            var model = new CategoryCreateVM();
            model.GeneralCategoryList = selectListItem;
            model.Category =  new Category();

            return View(model);
        }



        public JsonResult GetByAjax()
        {
            var var = _Manager.GetAll();

            return new JsonResult { Data = var, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }


        // ********** TEST END *****************//
        [HttpPost]
        public JsonResult Search(CategoryCreateVM vmObj)
        {

            var var = vmObj;

            var var2 = _Manager.GetBySearchItem(var.Category);
            return new JsonResult { Data = var2, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }




        public JsonResult GetbyID(int? id)
        {



            var var = _Manager.GetByID(id);


            return Json(var, JsonRequestBehavior.AllowGet);
        }

        private List<SelectListItem> GetSelectListItems()
        {
            var var = _Manager.GetSelectListItems();
            List<SelectListItem> SelectListItems = new List<SelectListItem>();
            SelectListItems.Add(new SelectListItem() { Value = "", Text = "Select..." });

            foreach (var var2 in var)
            {
                SelectListItems.Add(new SelectListItem()
                {
                    Value = var2.Id.ToString(),
                    Text = var2.Name
                });
            }
            return SelectListItems;
        }




        [HttpPost]
        public JsonResult Edit(CategoryCreateVM vmObj)
        {

            bool isUpdated = _Manager.Update(vmObj.Category);


            if (isUpdated)
            {

                status = true;

                message = "Update Successfully!!";


            }


            return new JsonResult { Data = new { status = status, message = message } };
        }



        public ActionResult Delete(int? id)
        {
            bool isDeleted = _Manager.Delete(id);

            message = "This Item Delted Successfully!!";

            return new JsonResult { Data = new { status = isDeleted, message = message } };


        }

    }
}