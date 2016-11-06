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
    public class OrganizationBranchController : Controller
    {

        private OrganizationBranchManager organizationBranchManager;
        public OrganizationBranchController() 
        {
            organizationBranchManager = new OrganizationBranchManager();
        }

        string message = "";
        bool status = false;

        // GET: OrganizationBranch
        public ActionResult Search()
        {
            var organizationSelectListItems = GetOrganizationSelectListItems();
            var model = new OrganizationBranchCreateVM();
            model.OrganizationList = organizationSelectListItems;
            model.OrganizationBranch = new OrganizationBranch();
            return View(model);
        }


        public JsonResult GetByAjax()
        {
            var organizationBranchs = organizationBranchManager.GetAll();

            return new JsonResult { Data = organizationBranchs, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }


        private List<SelectListItem> GetOrganizationSelectListItems()
        {
            var organizations = organizationBranchManager.GetOrganizationSelectListItems();
            List<SelectListItem> organizationSelectListItems = new List<SelectListItem>();
            organizationSelectListItems.Add(new SelectListItem() { Value = "", Text = "Select..." });

            foreach (var organization in organizations)
            {
                organizationSelectListItems.Add(new SelectListItem()
                {
                    Value = organization.Id.ToString(),
                    Text = organization.Name
                });
            }
            return organizationSelectListItems;
        }



        public JsonResult GetbyID(int? id)
        {



            var organization = organizationBranchManager.GetByID(id);


            return Json(organization, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]

        public JsonResult Create(OrganizationBranchCreateVM oraganizationCreateVm)
        {


            bool isSaved = false;

            if (ModelState.IsValid)
            {
                var organizationBranch = oraganizationCreateVm;

                isSaved = organizationBranchManager.Save(organizationBranch.OrganizationBranch);
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




        [HttpPost]
        public JsonResult Edit(OrganizationBranchCreateVM oraganizationBranchVM)
        {

            bool isUpdated = organizationBranchManager.Update(oraganizationBranchVM.OrganizationBranch);


            if (isUpdated)
            {

                status = true;

                message = "Update Successfully!!";


            }


            return new JsonResult { Data = new { status = status, message = message } };
        }



        public ActionResult Delete(int? id)
        {
            bool isDeleted = organizationBranchManager.Delete(id);

            message = "This Item Delted Successfully!!";

            return new JsonResult { Data = new { status = isDeleted, message = message } };


        }




    }
}