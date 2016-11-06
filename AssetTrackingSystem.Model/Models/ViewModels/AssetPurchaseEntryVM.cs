using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AssetTrackingSystem.Model.Models.ViewModels
{
    public class AssetPurchaseEntryVM
    {
        public AssetPurchaseEntry AssetPurchaseEntry { get; set; }
        public List<SelectListItem> GeneralCategoryList { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
        public List<SelectListItem> SubCategoryList { get; set; }
        public List<SelectListItem> ProductModelList { get; set; }
    }
}
