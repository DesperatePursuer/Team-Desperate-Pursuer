using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetTrackingSystem.Models;

namespace AssetTrackingSystem.Model.Models
{
    public class OrganizationBranch
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
        public string ShortName { get; set; }

        public virtual Organization Organization { get; set; }
        public int OrganizationId { get; set; }

        public bool IsDeleted { get; set; }


    }
}
