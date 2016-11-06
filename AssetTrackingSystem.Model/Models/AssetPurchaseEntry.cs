using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTrackingSystem.Model.Models
{
    public class AssetPurchaseEntry
    {
        public int Id { get; set; }
        public string PurchaseDate { get; set; }
        public string Vendor { get; set; }
        public ProductModel ProductModel { get; set; }
        public string ProductModelId { get; set; }
        public string WarrantyPeriod { get; set; }
        public double Price { get; set; }

        public int Qty { get; set; }
        public string SerialNo { get; set; }
        public bool IsDeleted { get; set; }
    }
}
