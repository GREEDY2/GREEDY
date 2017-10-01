using System.Collections.Generic;
using GREEDY.Models;
using System.Linq;
using System;
using System.Text.RegularExpressions;
using System.Data;
using System.Xml.Linq;

namespace GREEDY.DataManagers
{
    public class DataConverter : IDataConverter
    {
        private static ShopDistributor ShopDistributor => new ShopDistributor();

        public List<Item> ReceiptToItemList(Receipt receipt)
        {
            var shop = ShopDistributor.ReceiptDistributor(receipt);
            var ReceiptLinesToString = String.Join(Environment.NewLine, receipt.LinesOfText);
            List<Item> itemlList = new List<Item>();

            if (shop == "RIMI" || shop == "MAXIMA")
            {
                string pattern = @"([*]+)\n(.+)\n([*]+)";
                ReceiptLinesToString = Regex.Replace(ReceiptLinesToString, @"\r", "");

                Match match = Regex.Match(ReceiptLinesToString, pattern, RegexOptions.Singleline);
                if (match.Success)
                {
                    ReceiptLinesToString = match.Groups[2].Value;
                    pattern = @"([^..]*)([.]+)( \d+,\d\d)";

                    MatchCollection matches = Regex.Matches(ReceiptLinesToString, pattern, RegexOptions.Singleline);
                    foreach (Match m in matches)
                    {
                        itemlList.Add(new Item
                        {
                            Name = m.Groups[1].Value,
                            Price = decimal.Parse(m.Groups[3].Value),
                            Category = null
                        });
                    }
                }
                return itemlList;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public XElement ListToXml(List<Item> items)
        {
            XElement xmlElements = new XElement("items",
                items.Select(i => new XElement("item",
                new XAttribute("Name", i.Name),
                new XAttribute("Price", i.Price),
                new XAttribute("Category", i.Category))));
            return xmlElements;
        }

        public DataTable ListToDataTable(List<Item> items)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Price");
            dataTable.Columns.Add("Category");

            foreach (Item item in items)
            {
                DataRow dr = dataTable.NewRow();
                dr[0] = item.Name;
                dr[1] = item.Price;
                dr[2] = item.Category;
                dataTable.Rows.Add(dr);
            }
            return dataTable;
        }

    }
}