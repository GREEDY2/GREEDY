using System.ComponentModel.DataAnnotations;
using System;

namespace GREEDY.Data

{
    public class LoginSessionDataModel
    {   [Key]
        public Guid SessionID { get; set; }
        [Required]
        public UserDataModel User { get; set; }
    }
}
