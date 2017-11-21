using GREEDY.Data;
using GREEDY.DataManagers;
using GREEDY.Models;
using GREEDY.OCRs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace GREEDY.ReceiptCreatings
{
    public class ReceiptCreating : IReceiptCreating
    {
        private readonly IOcr _ocr;
        private readonly IShopManager _shops;

        public ReceiptCreating()
        {
            _ocr = new EmguOcr();
            _shops = new ShopManager(new DataBaseModel());
        }

        public ReceiptCreating(IOcr ocr, IShopManager shops)
        {
            _ocr = ocr;
            _shops = shops;
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

        public DateTime GetDateForReceipt(List<string> linesOfText)
        {
            var receiptLinesToString = String.Join(Environment.NewLine, linesOfText);
            string pattern = @"(\d{4}-\d{2}-\d{2})(\d{2})?";
            receiptLinesToString = Regex.Replace(receiptLinesToString, @"~", "-");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            Match match = Regex.Match(receiptLinesToString, pattern, RegexOptions.Singleline);
            if (match.Success)
            {
                return DateTime.Parse(match.Groups[1].Value, Thread.CurrentThread.CurrentCulture);
            }
            else
            {
                return DateTime.Now;
            }
        }

        public Shop GetShopFromData(List<string> linesOfText)
        {
            var shopTitle = String.Join(String.Empty, linesOfText.Take(4));
            var shops = _shops.GetExistingShop();

            foreach (Shop element in shops)
            {
                //TODO:Find function for UpperCase 
                if (shopTitle.ToUpper().Contains(element.Name.ToUpper()))
                {
                    return element;
                }
                else if (element.SubName!=null && shopTitle.ToUpper().Contains(element.SubName.ToUpper()) && element.SubName != String.Empty)
                {
                    return element;
                }
            }
            return new Shop { Name = "Neatpažinta" };
        }
    }
}