using System;
using System.Collections.Generic;

namespace GREEDY.Models
{
    public class Receipt
    {
        public float PercentageMatched { get; set; }
        public List<string> LinesOfText { get; set; }
        public List<Item> ItemsList { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public Shop Shop { get; set; }
    }
}