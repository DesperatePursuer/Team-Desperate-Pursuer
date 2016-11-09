using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AssetTrackingSystem.Models;

namespace AssetTrackingSystem.Model.Models.ViewModels
{
    public class AssetLocationVM
    {
        public AssetLocation AssetLocation { get; set; }
        public List<SelectListItem> OrganizationList { get; set; }
        public List<SelectListItem> OrganizationBranchList { get; set; }
    }
}
