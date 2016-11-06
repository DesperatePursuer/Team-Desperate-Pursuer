using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTrackingSystem.Model.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual GeneralCategory GeneralCategory { get; set; }
        public int GeneralCategoryId { get; set; }
        public bool IsDeleted { get; set; }


    }
}
