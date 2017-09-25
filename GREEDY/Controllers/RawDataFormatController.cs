using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GREEDY.Models;

namespace GREEDY.Controllers
{
    public class RawDataFormatController
    {
        private readonly string _data;

        public RawDataFormatController(Receipt receipt)
        {
            foreach (String line in receipt.LinesOfText)
            {
                _data += (line + Environment.NewLine);
            }
        }

        public DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Item");
            dt.Columns.Add("Price");
            dt.Columns.Add("Category");

            string pattern; 
            string input;  

            pattern = @"([*]+)\n(.+)\n([*]+)";
            input = _data;
            input = Regex.Replace(input, @"\r", ""); 

            Match match = Regex.Match(input, pattern, RegexOptions.Singleline);
            if (match.Success)
            {
                input = match.Groups[2].Value;
                pattern = @"([^..]*)([.]+)( \d+,\d\d)"; 
                
                MatchCollection matches = Regex.Matches(input, pattern, RegexOptions.Singleline);
                foreach (Match m in matches)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = m.Groups[1].Value; 
                    dr[1] = m.Groups[3].Value;
                    dr[2] = "Unknown";
                    dt.Rows.Add(dr);
                    dt.TableName = "ItemPriceList";
                }
            }
            
            return dt;
        }

        public List<Item> GetItemList()
        {
            List<Item> itemlList = new List<Item>();

            string pattern;
            string input;

            pattern = @"([*]+)\n(.+)\n([*]+)";
            input = _data;
            input = Regex.Replace(input, @"\r", "");

            Match match = Regex.Match(input, pattern, RegexOptions.Singleline);
            if (match.Success)
            {
                input = match.Groups[2].Value;
                pattern = @"([^..]*)([.]+)( \d+,\d\d)";

                MatchCollection matches = Regex.Matches(input, pattern, RegexOptions.Singleline);
                foreach (Match m in matches)
                {
                    itemlList.Add(new Item(m.Groups[1].Value, decimal.Parse(m.Groups[3].Value)));
                }
            }

            return itemlList;
        }

    }
}
