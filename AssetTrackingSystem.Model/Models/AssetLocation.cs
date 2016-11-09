using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTrackingSystem.Model.Models
{
    public class AssetLocation
    {
   
 
        public int Id { get; set; }
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Short Name")]
        public string ShortName { get; set; }
      
       
        [Required]
        [DisplayName("Organization Branch ")]
        public int OrganizationBranchId { get; set; }

        //public int OrganizationBranchId { get; set; }
        public virtual OrganizationBranch OrganizationBranch { get; set; }

        public bool IsDeleted { get; set; }


    }
}
