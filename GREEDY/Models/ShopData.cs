using Geocoding;
using System;

namespace GREEDY.Models
{
    public class ShopData
    {
        public string Name { get; set; }
        public Location Location { get; set; }
        public string Address { get; set; }
        public decimal Total { get; set; }
        public int ReceiptNumber { get; set; }
        public string Date { get; set; }
    }
}