﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GREEDY.Data
{
    public class CategoryDataModel
    {
        [Key]
        public string CategoryName { get; set; }
        public virtual ICollection<ItemDataModel> Items { get; set; }
    }
}
