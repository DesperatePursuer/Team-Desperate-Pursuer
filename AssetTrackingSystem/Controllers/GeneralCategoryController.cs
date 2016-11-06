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
    public class GeneralCategoryController : Controller
    {
        // GET: GeneralCategory

        private GeneralCategoryManager _Manager;

        public GeneralCategoryController()
        {
            _Manager = new GeneralCategoryManager();
        }




        string message = "";
        bool status = false;




        [HttpPost]

        public JsonResult Create(GeneralCategoryCreateVM vmObj)
        {


            bool isSaved = false;

            if (ModelState.IsValid)
            {
                var var = vmObj;

                isSaved = _Manager.Save(var.GeneralCategory);
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
            var model = new GeneralCategoryCreateVM();

            model.GeneralCategory = new GeneralCategory();

            return View(model);
        }



        public JsonResult GetByAjax()
        {
            var var = _Manager.GetAll();

            return new JsonResult { Data = var, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }


        // ********** TEST END *****************//
        [HttpPost]
        public JsonResult Search(GeneralCategoryCreateVM vmObj)
        {

            var var = vmObj;

            var var2 = _Manager.GetBySearchItem(var.GeneralCategory);
            return new JsonResult { Data = var2, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }




        public JsonResult GetbyID(int? id)
        {



            var var = _Manager.GetByID(id);


            return Json(var, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult Edit(GeneralCategoryCreateVM vmObj)
        {

            bool isUpdated = _Manager.Update(vmObj.GeneralCategory);


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