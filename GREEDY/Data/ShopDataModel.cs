using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace GREEDY.Data
{
    public class ShopDataModel
    {
        [Key]
        public int ShopId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Location { get; set; }
        public virtual ICollection<ReceiptDataModel> Receipts { get; set; }
    }
}
