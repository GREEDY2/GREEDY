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
            string pattern = @"([A-Za-z]{2}[A-Za-z]+.+)(\d+[\.\,]\d{2})(.[A|E|B|F|N|C]{1}(\b|\.))";
            List<Item> itemList = new List<Item>();
            string previous = String.Empty;
            List<string> sublist = new List<string>();
            Match match1;

            for (int i = 0; i < receipt.LinesOfText.Count; i++)
            {
                match1 = Regex.Match(receipt.LinesOfText[i], pattern, RegexOptions.Singleline);
                if (match1.Success)
                {
                    Match match2 = Regex.Match(previous, @"(?!(.+)?\d{6,}(.+)?)^.+$", RegexOptions.Multiline);
                    if (match2.Success)
                    {
                        itemList.Add(new Item
                        {
                            Name = previous + match1.Groups[1].Value,
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
            previous = Regex.Replace(previous, @"(\d+[\.\,]\d{2}.[A|E|B|F|N|C]{1}(\b|\.))", "$1" + Environment.NewLine);

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
            //TODO: if no item was created. Need to create a massage for user
            return itemList;
        }
    }
}