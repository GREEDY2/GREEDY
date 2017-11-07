using System;
using System.Collections.Generic;

namespace GREEDY.Models
{
    public class Receipt
    {
        public float PercentageMatched { get; set; }
        public List<string> LinesOfText { get; set; }
        public DateTime Date { get; set; }
        public Shop Shop { get; set; }
    }
}