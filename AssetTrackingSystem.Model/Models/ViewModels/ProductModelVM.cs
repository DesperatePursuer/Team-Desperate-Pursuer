using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AssetTrackingSystem.Model.Models.ViewModels
{
   public class ProductModelVM
    {
        public List<SelectListItem> GeneralCategoryList { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
        public List<SelectListItem> SubCategoryList { get; set; }
        public ProductModel ProductModel { get; set; }

    }
}
