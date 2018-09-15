using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GREEDY.Data
{
    public class CategoryDataModel
    {
        [Key] [Required] public string CategoryName { get; set; }

        public virtual ICollection<ItemDataModel> Items { get; set; }
    }
}