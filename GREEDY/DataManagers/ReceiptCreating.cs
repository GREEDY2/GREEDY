using GREEDY.Models;
using GREEDY.OCRs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace GREEDY.DataManagers
{
    public class ReceiptCreating : IReceiptCreating
    {
        private readonly IOcr _ocr;
        private readonly IShopManager _shops;

        public ReceiptCreating()
        {
            _ocr = new EmguOcr();
            _shops = new ShopManager();
        }

        public Receipt FullReceiptCreating(Bitmap image)
        {
            var linesOfText = _ocr.ConvertImage(image);
            var date = GetDateForReceipt(linesOfText);
            var shop = GetShopFromData(linesOfText);

            return new Receipt
            {
                Date = date,
                Shop = shop,
                LinesOfText = linesOfText
            };
        }

        public string GetDateForReceipt(List<string> linesOfText)
        {
            var receiptLinesToString = String.Join(Environment.NewLine, linesOfText);
            string pattern = @"(\d{4}-\d{2}-\d{2})(\d{2})?";
            receiptLinesToString = Regex.Replace(receiptLinesToString, @"~", "-");

            Match match = Regex.Match(receiptLinesToString, pattern, RegexOptions.Singleline);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                return DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        public Shop GetShopFromData(List<string> linesOfText)
        {
            var shopTitle = String.Join(String.Empty, linesOfText.Take(4));
            var shops = _shops.GetExistingShop();

            foreach ( Shop element in shops)
            {
                if (shopTitle.Contains(element.Name))
                {
                    return element;
                }
                else if (shopTitle.Contains(element.SubName) && element.SubName!= String.Empty)
                {
                    return element;
                }
            }
            return new Shop { Name = "Neatpažinta" };
        }
    }
}