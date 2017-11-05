using System.Collections.Generic;
using GREEDY.Models;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;

namespace GREEDY.DataManagers
{
    public class DataConverter : IDataConverter
    {
        private static ItemCategorization ItemCategorization => new ItemCategorization();

        public List<Item> ReceiptToItemList(Receipt receipt)
        {
            //Set correct number format
            CultureInfo customCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ",";
            Thread.CurrentThread.CurrentCulture = customCulture;

            //working with first item
            string pattern = @"([A-Za-z]{2}[A-Za-z]+.+)(\d+[\.\,]\d{2})(.[A|B|E|F|N]{1}(\b|\.))";
            List<Item> itemList = new List<Item>();
            string previous = String.Empty;
            List<string> sublist = new List<string>();
            Match match1;

            for (int i = 0; i < receipt.LinesOfText.Count; i++)
            //foreach (var line in receipt.LinesOfText)
            {
                match1 = Regex.Match(receipt.LinesOfText[i], pattern, RegexOptions.Singleline);
                if (match1.Success)
                {
                    Match match2 = Regex.Match(previous, @"(?!(.+)?\d{6,}(.+)?)^.+$", RegexOptions.Multiline);
                    if (match2.Success)
                    {
                        itemList.Add(new Item
                        {
                            Name = match2.Groups[1].Value + match1.Groups[1].Value,
                            Price = decimal.Parse(match1.Groups[2].Value.Replace(".", ",")),
                            Category = ItemCategorization.CategorizeSingleItem(match2.Groups[1].Value + match1.Groups[1].Value)
                        });
                        sublist = receipt.LinesOfText.GetRange(i + 1, receipt.LinesOfText.Count - i - 1);
                        break;
                    }
                    else
                    {
                        itemList.Add(new Item
                        {
                            Name = match1.Groups[1].Value,
                            Price = decimal.Parse(match1.Groups[2].Value.Replace(".", ",")),
                            Category = ItemCategorization.CategorizeSingleItem(match1.Groups[1].Value)
                        });
                        sublist = receipt.LinesOfText.GetRange(i + 1, receipt.LinesOfText.Count - i - 1);
                        break;
                    }
                }
                else
                {
                    previous = receipt.LinesOfText[i];
                }
            }

            //working with text
            previous = String.Join(Environment.NewLine, sublist);
            previous = Regex.Replace(previous, @"\r", "");
            previous = Regex.Replace(previous, @"\n", " ");
            previous = Regex.Replace(previous, "›", ",");
            previous = Regex.Replace(previous, @"(\d+[\.\,]\d{2}.[A|E|B|F|N]{1}(\b|\.))", "$1" + Environment.NewLine);

            //string pattern = @"\d+,\d{2}\b.[A|E|B|F|N]{1}\b(.+)(\d+,\d{2})(\b.[A|E|B|F|N]{1}\b)";
            MatchCollection match = Regex.Matches(previous, pattern, RegexOptions.Multiline);
            if (match.Count != 0)
            {
                foreach (Match m in match)
                {
                    itemList.Add(new Item
                    {
                        Name = m.Groups[1].Value.Replace("\n", string.Empty),
                        Price = decimal.Parse(m.Groups[2].Value.Replace(".", ",")),
                        Category = ItemCategorization.CategorizeSingleItem(m.Groups[1].Value)
                    });
                }
            }


            Console.WriteLine(receipt.Shop.Name);
            Console.WriteLine(receipt.Date);

            return itemList;

            //return itemList;

            //////////////////IDictionary<string, string> replaceDiction = new Dictionary<string, string>()
            //////////////////{
            //////////////////    { @"\r",""},
            //////////////////    { @"\n"," "},
            //////////////////    { "›",","}
            //////////////////};

            //////////////////replace every pair in dictionary
            ////////////////var receiptLinesToString = String.Join(Environment.NewLine, receipt.LinesOfText);
            //////////////////var regex = new Regex(String.Join("|", replaceDiction.Keys));
            //////////////////receiptLinesToString = regex.Replace(receiptLinesToString, r => replaceDiction[r.Value]);


            //////////////////string pattern = @"\d+,\d{2}\b.[A|E|B|F|N]{1}\b(.+)(\d+,\d{2})(\b.[A|E|B|F|N]{1}\b)";
            ////////////////string pattern = @"(.+)(\d+,\d{2})(.[A|E|B|F|N]{1}(\b|\.))";
            ////////////////MatchCollection match = Regex.Matches(receiptLinesToString, pattern, RegexOptions.Multiline);
            ////////////////if (match.Count != 0)
            ////////////////{
            ////////////////    foreach (Match m in match)
            ////////////////    {
            ////////////////        //var sss = receipt.LinesOfText.GetEnumerator().Current;
            ////////////////        itemList.Add(new Item
            ////////////////        {
            ////////////////            Name = m.Groups[1].Value.Replace("\n", string.Empty),
            ////////////////            Price = decimal.Parse(m.Groups[2].Value),
            ////////////////            Category = ItemCategorization.CategorizeSingleItem(m.Groups[1].Value)
            ////////////////        });
            ////////////////    }
            ////////////////}
            ////////////////return itemList;
            ////string pattern = @"(\n[A-Za-z]{2}[A-Za-z]+.+)(\d+(,)\d\d).[A|E|B|F|N]{1}\b\n";
            //foreach (string line in receipt.LinesOfText)
            //{
            //    Match match = Regex.Match(Regex.Replace(line, @"\r", ""), pattern, RegexOptions.Multiline);
            //    if (match.Success)
            //    {
            //        //var sss = receipt.LinesOfText.GetEnumerator().Current;
            //        itemList.Add(new Item
            //        {
            //            Name = match.Groups[1].Value.Replace("\n", string.Empty),
            //            Price = decimal.Parse(match.Groups[2].Value),
            //            Category = ItemCategorization.CategorizeSingleItem(match.Groups[1].Value)
            //        });
            //    }
            //}
            //return itemList;
            //catch (Exception e)
            //{
            //    Console.WriteLine("Problem with receipt text: {0}", e);
            //    throw new NotImplementedException();
            //}





            //    //TODO: change strings to dictionary
            //    //TODO: create extension method to pass only shop name and get itemList
            //    if (receipt.Shop.Name == "MAXIMA")
            //    {
            //        string pattern = @"\b(([A-Z]|#)(\d){8})(.+)\n(PVM\b)";
            //        receiptLinesToString = Regex.Replace(receiptLinesToString, @"\r", "");

            //        Match match = Regex.Match(receiptLinesToString, pattern, RegexOptions.Singleline);
            //        if (match.Success)
            //        {
            //            var productList = match.Groups[4].Value;
            //            productList = Regex.Replace(productList, @"\n", " ");
            //            productList = Regex.Replace(productList, @"(\d+(,)\d\d).[A|E|B|F|N]{1}\b", "$1" + Environment.NewLine);

            //            pattern = @"([A-Za-z]{2}[A-Za-z]+.+)(\d+(,)\d\d)\r\n";
            //            MatchCollection matches = Regex.Matches(productList, pattern, RegexOptions.Multiline);
            //            foreach (Match m in matches)
            //            {
            //                itemList.Add(new Item
            //                {
            //                    Name = m.Groups[1].Value.Replace("\n", string.Empty),
            //                    Price = decimal.Parse(m.Groups[2].Value),
            //                    Category = ItemCategorization.CategorizeSingleItem(m.Groups[1].Value)
            //                });
            //            }
            //            return itemList;
            //        }
            //        return itemList;
            //    }
            //    else if (receipt.Shop.Name == "IKI")
            //    {
            //        string pattern = @"\b(([A-Z]{2})(\d){9})(.+)(Prekiautojo\b|ID\b)";
            //        receiptLinesToString = Regex.Replace(receiptLinesToString, @"\r", "");

            //        Match match = Regex.Match(receiptLinesToString, pattern, RegexOptions.Singleline);
            //        if (match.Success)
            //        {
            //            var productList = match.Groups[4].Value;
            //            productList = Regex.Replace(productList, @"\n", " ");
            //            productList = Regex.Replace(productList, @"›", ",");
            //            productList = Regex.Replace(productList, @"(\d+(,)\d\d).[A|E|B|F|N]{1}\b", "$1" + Environment.NewLine);

            //            pattern = @"([A-Za-z]{2}[A-Za-z]+.+)(\d+(,)\d\d)\r\n";
            //            MatchCollection matches = Regex.Matches(productList, pattern, RegexOptions.Multiline);
            //            foreach (Match m in matches)
            //            {
            //                itemList.Add(new Item
            //                {
            //                    Name = m.Groups[1].Value.Replace("\n", string.Empty),
            //                    Price = decimal.Parse(m.Groups[2].Value),
            //                    Category = ItemCategorization.CategorizeSingleItem(m.Groups[1].Value)
            //                });
            //            }
            //            return itemList;
            //        }
            //        return itemList;
            //    }
            //    else
            //    {
            //        throw new NotImplementedException();
            //    }
            //}



            //// TODO
            //// this doesnt belong in this class, can be moved to a static method, maybe an extension method
            //public XElement ListToXml(List<Item> items)
            //{
            //    XElement xmlElements = new XElement("items",
            //        items.Select(i => new XElement("item",
            //        new XAttribute("Name", i.Name),
            //        new XAttribute("Price", i.Price),
            //        new XAttribute("Category", i.Category))));
            //    return xmlElements;
            //}

            //// TODO
            //// this doesnt belong in this class, can be moved to a static method, maybe an extension method
            //public DataTable ListToDataTable(List<Item> items)
            //{
            //    DataTable dataTable = new DataTable();
            //    dataTable.Columns.Add("Name");
            //    dataTable.Columns.Add("Price");
            //    dataTable.Columns.Add("Category");

            //    foreach (Item item in items)
            //    {
            //        DataRow dr = dataTable.NewRow();
            //        dr[0] = item.Name;
            //        dr[1] = item.Price;
            //        dr[2] = item.Category;
            //        dataTable.Rows.Add(dr);
            //    }
            //    return dataTable;
            //}
        }
    }
}