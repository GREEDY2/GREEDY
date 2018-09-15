using System.ComponentModel.DataAnnotations;

namespace GREEDY.Data
{
    public class ItemDataModel
    {
        [Key] public int ItemId { get; set; }

        [Required] public decimal Price { get; set; }

        [Required] public string Name { get; set; }

        [Required] public virtual CategoryDataModel Category { get; set; }

        public virtual ReceiptDataModel Receipt { get; set; }
    }
}