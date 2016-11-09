using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTrackingSystem.Model.Models
{
   public class AssetRegistration
    {
        public int id { get; set; }
        public string AssetId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int LocationId { get; set; }
        public AssetLocation AssetLocation { get; set; }
    }
}
