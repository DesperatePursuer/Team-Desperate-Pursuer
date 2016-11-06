using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssetTrackingSystem.Model.Models;

namespace AssetTrackingSystem.Models.ViewModels
{
    public class OraganizationCreateVM
    {
        public Organization Oraganization { get; set; }

        public OrganizationBranch OrganizationBranch { get; set; }

        public List<Organization> Oragnizations { get; set; }
    }
}