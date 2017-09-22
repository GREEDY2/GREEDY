using GREEDY.Interfaces;
using System.Collections.Generic;

namespace GREEDY.Models
{
    public class Receipt : IReceipt
    {
        public float PercentageMatched { get; set; }
        public List<string> LinesOfText { get; set; }
    }
}
