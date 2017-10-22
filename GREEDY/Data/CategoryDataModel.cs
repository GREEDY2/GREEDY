using System.ComponentModel.DataAnnotations;

namespace GREEDY.Data
{
    public class CategoryDataModel
    {
        [Key]
        public string Keyword { get; set; }
        [Required]
        public string Category { get; set; }
    }
}
