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
        private static ItemCategorization ItemCategorization => new ItemCategorization();

        public List<Item> ReceiptToItemList(Receipt receipt)
        {
            var shop = ShopDistributor.ReceiptDistributor(receipt);
            var receiptLinesToString = String.Join(Environment.NewLine, receipt.LinesOfText);
            List<Item> itemList = new List<Item>();

            if (shop == "MAXIMA")
            {
                string pattern = @"\b(([A-Z]|#)(\d){8})(.+)\n(PVM\b)";
                receiptLinesToString = Regex.Replace(receiptLinesToString, @"\r", "");

                Match match = Regex.Match(receiptLinesToString, pattern, RegexOptions.Singleline);
                if (match.Success)
                {
                    var productList = match.Groups[4].Value;
                    productList = Regex.Replace(productList, @"\n", " ");
                    productList = Regex.Replace(productList, @"(\d+(,)\d\d).[A|E|B|F|N]{1}\b", "$1" + Environment.NewLine);

                    //words begin at least with 3 letters, after that it takes everything until it find price with new line
                    pattern = @"([A-Za-z]{2}[A-Za-z]+.+)(\d+(,)\d\d)\r\n";
                    MatchCollection matches = Regex.Matches(productList, pattern, RegexOptions.Multiline);
                    foreach (Match m in matches)
                    {
                        itemList.Add(new Item
                        {
                            Name = m.Groups[1].Value.Replace("\n", string.Empty),
                            Price = decimal.Parse(m.Groups[2].Value),
                            Category = ItemCategorization.CategorizeSingleItem(m.Groups[1].Value)
                        });
                    }
                    return itemList;
                }
                return itemList;
            }
            else if(shop == "IKI")
            {
                string pattern = @"\b(([A-Z]{2})(\d){9})(.+)(Prekiautojo\b|ID\b)";
                receiptLinesToString = Regex.Replace(receiptLinesToString, @"\r", "");

                Match match = Regex.Match(receiptLinesToString, pattern, RegexOptions.Singleline);
                if (match.Success)
                {
                    var productList = match.Groups[4].Value;
                    productList = Regex.Replace(productList, @"\n", " ");
                    productList = Regex.Replace(productList, @"›", ",");
                    productList = Regex.Replace(productList, @"(\d+(,)\d\d).[A|E|B|F|N]{1}\b", "$1" + Environment.NewLine);

                    pattern = @"([A-Za-z]{2}[A-Za-z]+.+)(\d+(,)\d\d)\r\n";
                    MatchCollection matches = Regex.Matches(productList, pattern, RegexOptions.Multiline);
                    foreach (Match m in matches)
                    {
                        itemList.Add(new Item
                        {
                            Name = m.Groups[1].Value.Replace("\n", string.Empty),
                            Price = decimal.Parse(m.Groups[2].Value),
                            Category = ItemCategorization.CategorizeSingleItem(m.Groups[1].Value)
                        });
                    }
                    return itemList;
                }
                return itemList;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        // TODO
        // this doesnt belong in this class, can be moved to a static method, maybe an extension method
        public XElement ListToXml(List<Item> items)
        {
            XElement xmlElements = new XElement("items",
                items.Select(i => new XElement("item",
                new XAttribute("Name", i.Name),
                new XAttribute("Price", i.Price),
                new XAttribute("Category", i.Category))));
            return xmlElements;
        }

        // TODO
        // this doesnt belong in this class, can be moved to a static method, maybe an extension method
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
