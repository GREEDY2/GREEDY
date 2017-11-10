using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GREEDY.Models
{
    public class GraphData
    {
        public decimal ReceiptPrice { get; set; }
        public decimal ItemPrice { get; set; }
        public string Time { get; set; }
        public DateTime Date { get; set; }
        public string FullDateTimeString { get { return Date.ToString("yyyy-MM-dd HH:mm"); } }

    }
}
