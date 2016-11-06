using AssetTrackingSystem.BLL;
using AssetTrackingSystem.DAL.Context;
using AssetTrackingSystem.DAL;
using AssetTrackingSystem.Models;
using AssetTrackingSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AssetTrackingSystem.Controllers
{
    public class OrganizationController : Controller
    {
       

        private OrganizationManager organizationManager;

        public OrganizationController()
        {
            organizationManager = new OrganizationManager();
        }

        // GET: Organization


        string message = "";
        bool status = false;


 

        [HttpPost]
      
        public JsonResult Create(OraganizationCreateVM oraganizationCreateVm)
        {

            
            bool isSaved = false;

            if (ModelState.IsValid)
            {
                var organization = oraganizationCreateVm;

                  isSaved = organizationManager.Save(organization.Oraganization);
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


// ********** TEST START *****************//

        public ActionResult Home()
        {
            var model = new OraganizationCreateVM();
            model.Oraganization = new Organization();

            return View(model);
        }


        public JsonResult GetByAjax()
        {
            var organizations = organizationManager.GetAll();

            return new JsonResult { Data = organizations, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }


        // ********** TEST END *****************//
        [HttpPost]
        public JsonResult Search(OraganizationCreateVM  oraganizationCreateVm)
        {

            var organization = oraganizationCreateVm;

            var organizations = organizationManager.GetOrganizationBySearchItem(organization.Oraganization);
            return new JsonResult { Data = organizations, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

     


        public JsonResult GetbyID(int? id)
        {



            var organization = organizationManager.GetByID(id);

           
            return Json(organization, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult Edit(OraganizationCreateVM oraganizationCreateVm)
        {

            bool isUpdated = organizationManager.Update(oraganizationCreateVm.Oraganization);
           

            if (isUpdated)
            {

                status = true;

                message = "Update Successfully!!";


            }


            return new JsonResult { Data = new { status = status, message = message } };
        }

       

        public ActionResult Delete(int? id)
        {
            bool isDeleted = organizationManager.Delete(id);

            message = "This Item Delted Successfully!!";

            return new JsonResult { Data = new { status = isDeleted, message = message } };


        }
    }
}