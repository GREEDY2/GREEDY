using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace GREEDY.Data
{
    public class UserDataModel
    {
        [Key]
        [MaxLength(255)]
        public string Username { get; set; }
        [Required]
        [Index(IsUnique =true)]
        [MaxLength(255)]
        public string Email { get; set; }
        [Required]
        public string FullName {get;set;}
        public string Password { get; set; }
        public bool IsFacebookUser { get; set; }
        public virtual ICollection<ReceiptDataModel> Receipts { get; set; }

    }
}
