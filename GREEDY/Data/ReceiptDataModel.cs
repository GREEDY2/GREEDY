using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace GREEDY.Data
{
    public class ReceiptDataModel
    {
        [Key]
        public int ReceiptId { get; set; }
        public decimal Total { get; set; }
        [Column(TypeName ="Date")]
        public DateTime Date { get; set; }
        [Required]
        public virtual UserDataModel User { get; set; }
        [Required]
        public virtual ShopDataModel Shop { get; set; }
        [Required]
        public virtual ICollection<ItemDataModel> Items { get; set; }
    }
}
