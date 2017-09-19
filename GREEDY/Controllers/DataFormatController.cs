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
    class DataFormatController
    {
        private readonly string _data;

        public DataFormatController(Receipt receipt)
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

            string pattern; //a pattern for regex to match
            string input;   //a string to compare for regex

            pattern = @"([*]+)\n(.+)\n([*]+)";//the pattern reads the stars and the text between
            input = _data;
            input = Regex.Replace(input, @"\r", ""); //removing the "\r" so it does not disturb the regex

            Match match = Regex.Match(input, pattern, RegexOptions.Singleline);
            if (match.Success)
            {
                //match.Groups[2].Value string contains only items and their prices
                input = match.Groups[2].Value;
                pattern = @"([^..]*)([.]+)( \d+,\d\d)"; //the pattern reads the item name, the dots and the price

                //going throug all mathes of items
                MatchCollection matches = Regex.Matches(input, pattern, RegexOptions.Singleline);
                foreach (Match m in matches)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = m.Groups[1].Value; //item name
                    dr[1] = m.Groups[3].Value; //item price
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }
}
