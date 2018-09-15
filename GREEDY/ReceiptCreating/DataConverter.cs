using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using GREEDY.Models;

namespace GREEDY.ReceiptCreating
{
    public class DataConverter : IDataConverter
    {
        public List<Item> ReceiptToItemList(Receipt receipt)
        {
            //Set correct number format
            var customCulture = (CultureInfo) Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ",";
            Thread.CurrentThread.CurrentCulture = customCulture;

            //working with first item
            var pattern = @"([\p{L}]{2}[\p{L}]+.+)([\-, ]\d+[\.\,]\d{2})(.[A|E|B|F|N|C]{1}(\b|\.))";
            var pattern2 = @"([\p{L}])\.([\p{L}])";
            var itemList = new List<Item>();
            var previous = string.Empty;
            var sublist = new List<string>();
            var category = string.Empty;

            for (var i = 0; i < receipt.LinesOfText.Count; i++)
            {
                var match1 = Regex.Match(receipt.LinesOfText[i], pattern, RegexOptions.Singleline);
                if (match1.Success)
                {
                    var match2 = Regex.Match(previous, @"(?!(.+)?\d{5,}(.+)?)^.+$", RegexOptions.Multiline);
                    if (match2.Success)
                    {
                        if (decimal.Parse(match1.Groups[2].Value.Replace(".", ",")) < 0) category = "discount";
                        itemList.Add(new Item
                        {
                            Name = Regex.Replace(Regex.Match(previous, @"[\p{L}]{2}[\p{L}]+.+")
                                                 + match1.Groups[1].Value, pattern2, "$1" + " " + "$2"),
                            Price = decimal.Parse(match1.Groups[2].Value.Replace(".", ",")),
                            Category = category
                        });
                        category = string.Empty;
                        sublist = receipt.LinesOfText.GetRange(i + 1, receipt.LinesOfText.Count - i - 1);
                        break;
                    }

                    if (decimal.Parse(match1.Groups[2].Value.Replace(".", ",")) < 0) category = "discount";
                    itemList.Add(new Item
                    {
                        Name = Regex.Replace(match1.Groups[1].Value, pattern2, "$1" + " " + "$2"),
                        Price = decimal.Parse(match1.Groups[2].Value.Replace(".", ",")),
                        Category = category
                    });
                    category = string.Empty;
                    sublist = receipt.LinesOfText.GetRange(i + 1, receipt.LinesOfText.Count - i - 1);
                    break;
                }

                previous = receipt.LinesOfText[i];
            }

            //working with text check if any iteams was found
            if (sublist.Count == 0) return itemList;
            //working with text
            previous = string.Join(Environment.NewLine, sublist);
            previous = Regex.Replace(previous, @"\r", "");
            previous = Regex.Replace(previous, @"\n", " ");
            previous = Regex.Replace(previous, "›", ",");
            previous = Regex.Replace(previous, "»", "-");
            previous = Regex.Replace(previous, pattern2, "$1" + " " + "$2");
            previous = Regex.Replace(previous, @"([\-]?\d+[\.\,]\d{2}).[A|E|B|F|N|C]{1}(\b|\.)",
                "$1" + Environment.NewLine);
            pattern = @"([\p{L}]{2}[\p{L}]+.+)([\-, ]\d+[\.\,]\d{2})\r\n";
            var match = Regex.Matches(previous, pattern, RegexOptions.Multiline);
            if (match.Count == 0) return itemList;
            foreach (Match m in match)
            {
                if (decimal.Parse(m.Groups[2].Value.Replace(".", ",")) < 0) category = "discount";
                itemList.Add(new Item
                {
                    Name = m.Groups[1].Value.Replace("\n", string.Empty),
                    Price = decimal.Parse(m.Groups[2].Value.Replace(".", ",")),
                    Category = category
                });
                category = string.Empty;
            }

            return itemList;

        }
    }
}