using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GREEDY.Interfaces;

namespace GREEDY.Models
{
    public class Item : IItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        public Item(string name = "", decimal price = 0, string category = "")
        {
            Name = name;
            Price = price;
            Category = category;
        }
    }
}
