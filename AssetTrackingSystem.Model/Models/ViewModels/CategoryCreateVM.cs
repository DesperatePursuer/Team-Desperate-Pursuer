using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AssetTrackingSystem.Model.Models.ViewModels
{
    public class CategoryCreateVM
    {
        public Category Category { get; set; }
        public List<SelectListItem> GeneralCategoryList { get; set; }
    }
}
