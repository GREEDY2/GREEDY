using System.Collections.Generic;
using GREEDY.Models;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using System.Linq;

namespace GREEDY.ReceiptCreatings
{
    public class DataConverter : IDataConverter
    {
        private readonly ItemCategorization _itemCategorization;

        public DataConverter()
        {
            _itemCategorization = new ItemCategorization();
        }

        public List<Item> ReceiptToItemList(Receipt receipt)
        {
            //Set correct number format
            CultureInfo customCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ",";
            Thread.CurrentThread.CurrentCulture = customCulture;

            //working with first item
            string pattern = @"([\p{L}]{2}[\p{L}]+.+)([\-, ]\d+[\.\,]\d{2})(.[A|E|B|F|N|C]{1}(\b|\.))";
            string pattern2 = @"([\p{L}])\.([\p{L}])";
            List<Item> itemList = new List<Item>();
            string previous = String.Empty;
            List<string> sublist = new List<string>();
            Match match1;

            for (int i = 0; i < receipt.LinesOfText.Count; i++)
            {
                match1 = Regex.Match(receipt.LinesOfText[i], pattern, RegexOptions.Singleline);
                if (match1.Success)
                {
                    Match match2 = Regex.Match(previous, @"(?!(.+)?\d{5,}(.+)?)^.+$", RegexOptions.Multiline);
                    if (match2.Success)
                    {
                        itemList.Add(new Item
                        {
                            Name = Regex.Replace(previous + match1.Groups[1].Value, pattern2, "$1" + " " + "$2"),
                            Price = decimal.Parse(match1.Groups[2].Value.Replace(".", ",")),
                            Category = String.Empty
                            //ItemCategorization.CategorizeSingleItem(match2.Groups[1].Value + match1.Groups[1].Value)
                        });
                        sublist = receipt.LinesOfText.GetRange(i + 1, receipt.LinesOfText.Count - i - 1);
                        break;
                    }
                    else
                    {
                        itemList.Add(new Item
                        {
                            Name = Regex.Replace(match1.Groups[1].Value, pattern2, "$1" + " " + "$2"),
                            Price = decimal.Parse(match1.Groups[2].Value.Replace(".", ",")),
                            Category = String.Empty
                            //ItemCategorization.CategorizeSingleItem(match1.Groups[1].Value)
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
            //check if any iteams was found
            if (sublist.Count != 0)
            {
                //working with text
                previous = String.Join(Environment.NewLine, sublist);
                previous = Regex.Replace(previous, @"\r", "");
                previous = Regex.Replace(previous, @"\n", " ");
                previous = Regex.Replace(previous, "›", ",");
                previous = Regex.Replace(previous, pattern2, "$1" + " " + "$2");
                previous = Regex.Replace(previous, @"([\-]?\d+[\.\,]\d{2}).[A|E|B|F|N|C]{1}(\b|\.)", "$1" + Environment.NewLine);

                pattern = @"([\p{L}]{2}[\p{L}]+.+)([\-, ]\d+[\.\,]\d{2})\r\n";

                MatchCollection match = Regex.Matches(previous, pattern, RegexOptions.Multiline);
                if (match.Count != 0)
                {
                    foreach (Match m in match)
                    {
                        itemList.Add(new Item
                        {
                            Name = m.Groups[1].Value.Replace("\n", string.Empty),
                            Price = decimal.Parse(m.Groups[2].Value.Replace(".", ",")),
                            Category = String.Empty
                            //ItemCategorization.CategorizeSingleItem(m.Groups[1].Value)
                        });
                    }
                }
                return itemList;
            }
            else
            {
                return itemList;
            }
        }

        public List<Item> CategorizeItems(List<Item> itemList)
        {
            List<ItemInfo> newData;
            newData = itemList.Select(x => new ItemInfo { Category = String.Empty, Text = x.Name, Prob = 0 }).ToList();
            newData = _itemCategorization.CategorizeAllItems(newData);
            foreach (Item item in itemList)
            {
                foreach (ItemInfo itemInfo in newData)
                {
                    if(item.Name == itemInfo.Text)
                    {
                        item.Category = itemInfo.Category;
                    }
                }
            }
            return itemList;
        }
    }
}