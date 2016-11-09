using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssetTrackingSystem.BLL.Organization;
using AssetTrackingSystem.Model.Models;
using AssetTrackingSystem.Model.Models.ViewModels;

namespace AssetTrackingSystem.Controllers
{
    public class AssetLocationController : Controller
    {
        // GET: AssetLocation
        private AssetLocationManager _Manager;

        public AssetLocationController()
        {
            _Manager = new AssetLocationManager();
        }




        string message = "";
        bool status = false;




        [HttpPost]

        public JsonResult Create(AssetLocationVM vmObj)
        {


            bool isSaved = false;

            if (ModelState.IsValid)
            {
                var var = vmObj;

                isSaved = _Manager.Save(var.AssetLocation);
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
            var model = new AssetLocationVM();
            model.OrganizationList = selectListItem;
            model.AssetLocation = new AssetLocation();

            return View(model);
        }



        public JsonResult GetByAjax()
        {
            var var = _Manager.GetAll();

            return new JsonResult { Data = var, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }


        // ********** TEST END *****************//
        [HttpPost]
        public JsonResult Search(AssetLocationVM vmObj)
        {

            var var = vmObj;

            var var2 = _Manager.GetBySearchItem(var.AssetLocation);
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

        public JsonResult GetOrganizationBranchByOrganization(int id)
        {

            int gorganizationId = id;
            var organizationBranches = _Manager
                .GetOrganizationBranchByOrganization(gorganizationId)
                .Select(c => new { Id = c.Id, Name = c.Name });

            return Json(organizationBranches, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult Edit(AssetLocationVM vmObj)
        {

            bool isUpdated = _Manager.Update(vmObj.AssetLocation);


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