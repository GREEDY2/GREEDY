﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using GREEDY.Models;
using GREEDY.OCRs;

namespace GREEDY.ReceiptCreating
{
    public class ReceiptMaking : IReceiptMaking
    {
        private readonly IOcr _ocr;
        private readonly IShopDetection _shopDetection;

        public ReceiptMaking()
        {
            _ocr = new EmguOcr();
            _shopDetection = new ShopDetection();
        }

        public ReceiptMaking(IOcr ocr, IShopDetection shopDetection)
        {
            _ocr = ocr;
            _shopDetection = shopDetection;
        }

        public Receipt FullReceiptCreating(Bitmap image)
        {
            var linesOfText = _ocr.ConvertImage(image);
            if (linesOfText == null) return null;
            var date = GetDateForReceipt(linesOfText);
            var shop = _shopDetection.GetShopFromData(linesOfText.Take(4).ToList());

            return new Receipt
            {
                ReceiptDate = date,
                UpdateDate = DateTime.Today,
                Shop = shop,
                LinesOfText = linesOfText
            };

        }

        public DateTime? GetDateForReceipt(List<string> linesOfText)
        {
            var receiptLinesToString = string.Join(Environment.NewLine, linesOfText);
            const string pattern = @"(\d{2}(\d{2})?[-,\\]\d{2}[-,\\]\d{2}(\d{2})?)";
            receiptLinesToString = Regex.Replace(receiptLinesToString, @"~", "-");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var match = Regex.Match(receiptLinesToString, pattern, RegexOptions.Singleline);
            if (match.Success)
                return DateTime.Parse(match.Groups[1].Value, Thread.CurrentThread.CurrentCulture);
            return null;
        }
    }
}