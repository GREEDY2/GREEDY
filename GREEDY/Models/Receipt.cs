using System;
using GREEDY.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GREEDY.Models
{
    public class Receipt : IReceipt
    {
        public float PercentageMatched { get; set; }
        public List<string> LinesOfText { get; set; }
    }
}
