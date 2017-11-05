using System.ComponentModel.DataAnnotations;

namespace GREEDY.Data
{
    public class ItemDataModel
    {
        [Key]
        public int ItemId { get; set; }
        public string Category { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ReceiptDataModel Receipt { get; set; }
    }
}
