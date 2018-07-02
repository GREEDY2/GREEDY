using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Geocoding;

namespace GREEDY.Data
{
    public class ShopDataModel
    {
        [Key] public int ShopId { get; set; }

        [Required] public string Name { get; set; }

        public Location Location { get; set; }
        public string Address { get; set; }
        public string SubName { get; set; }
        public virtual ICollection<ReceiptDataModel> Receipts { get; set; }
    }
}