﻿using System.Collections.Generic;
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

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ",";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            if (shop == "RIMI" || shop == "MAXIMA")
            {
                string pattern = @"([*]+)\n(.+)\n([*]+)";
                receiptLinesToString = Regex.Replace(receiptLinesToString, @"\r", "");

                Match match = Regex.Match(receiptLinesToString, pattern, RegexOptions.Singleline);
                if (match.Success)
                {
                    receiptLinesToString = match.Groups[2].Value;
                    pattern = @"([^..]*)([.]+)( \d+,\d\d)";

                    MatchCollection matches = Regex.Matches(receiptLinesToString, pattern, RegexOptions.Singleline);
                    foreach (Match m in matches)
                    {
                        itemList.Add(new Item
                        {
                            Name = m.Groups[1].Value.Replace("\n", string.Empty),
                            Price = decimal.Parse(m.Groups[3].Value),
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