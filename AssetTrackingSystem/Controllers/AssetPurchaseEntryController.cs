using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssetTrackingSystem.BLL.Operation;
using AssetTrackingSystem.Model.Models;
using AssetTrackingSystem.Model.Models.ViewModels;

namespace AssetTrackingSystem.Controllers
{
    public class AssetPurchaseEntryController : Controller
    {
        // GET: AssetPurchaseEntry
        private AssetPurchaseEntryManager _Manager;

        public AssetPurchaseEntryController()
        {
            _Manager = new AssetPurchaseEntryManager();
        }




        string message = "";
        bool status = false;




        [HttpPost]

        public JsonResult Create(AssetPurchaseEntryVM vmObj)
        {


            bool isSaved = false;

            if (ModelState.IsValid)
            {
                var var = vmObj;

                isSaved = _Manager.Save(var.AssetPurchaseEntry);
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
            var model = new AssetPurchaseEntryVM();
            model.GeneralCategoryList = selectListItem;
            model.AssetPurchaseEntry = new AssetPurchaseEntry();

            return View(model);
        }



        public JsonResult GetByAjax()
        {
            var var = _Manager.GetAll();

            return new JsonResult { Data = var, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }


        // ********** TEST END *****************//
        [HttpPost]
        public JsonResult Search(AssetPurchaseEntryVM vmObj)
        {

            var var = vmObj;

            var var2 = _Manager.GetBySearchItem(var.AssetPurchaseEntry);
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

        public JsonResult GetCategoriesByGeneralCategory(int id)
        {

            int generalCateogryId = id;
            var categories = _Manager
                .GetCategoriesByGeneralCategory(generalCateogryId)
                .Select(c => new { Id = c.Id, Name = c.Name });

            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubCategoryByCategory(int id)
        {

            int generalCateogryId = id;
            var categories = _Manager
                .GetSubCategoryByCategory(generalCateogryId)
                .Select(c => new { Id = c.Id, Name = c.Name });

            return Json(categories, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductModelBySubCategory(int id)
        {

            int subgeneralCateogryId = id;
            var categories = _Manager
                .GetProductModelBySubCategory(subgeneralCateogryId)
                .Select(c => new { Id = c.Id, Name = c.Name });

            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(AssetPurchaseEntryVM vmObj)
        {

            bool isUpdated = _Manager.Update(vmObj.AssetPurchaseEntry);


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