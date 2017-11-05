using GREEDY.Data;
using GREEDY.Models;
using GREEDY.OCRs;
using Newtonsoft.Json;
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

        public ReceiptCreating()
        {
            _ocr = new EmguOcr();
            //TODO: !!! need to find better place or create new class to fill all tables data as we need !!!
            AddDataToShopDataTable();
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
            string pattern = @"(\d{4}-\d{2}-\d{2})";
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

        private void AddDataToShopDataTable()
        {
            //"Environments.AppConfig.Shops
            dynamic dynJson = JsonConvert.DeserializeObject<List<Shop>>(
                @"[
                    {'Name': 'IKI','Adress': '','Subname': 'PALINK'},
                    {'Name': 'MAXIMA','Adress': '', 'Subname': ''},
                    {'Name': 'RIMI','Adress': '','Subname': ''},
                    {'Name': 'LIDL','Adress': '','Subname': ''}
                ]");

            using (DataBaseModel context = new DataBaseModel())
            {
                foreach (var item in dynJson)
                {
                    context.Set<ShopDataModel>()
                        .Add(new ShopDataModel() { Name = item.Name, Location = item.Location, SubName = item.SubName });
                    context.SaveChanges();
                }
            }
        }

        public static List<Shop> GetExistingShop()
        {
            using (DataBaseModel context = new DataBaseModel())
            {
                return context.Set<ShopDataModel>()
                    .Select(x => new Shop() { Name = x.Name, Location = x.Location, SubName = x.SubName })
                    .ToList();
            }
        }

        public Shop GetShopFromData(List<string> linesOfText)
        {
            var shopTitle = String.Join(String.Empty, linesOfText.Take(4));
            var shops = GetExistingShop();

            foreach ( Shop element in shops)
            {
                if (shopTitle.Contains(element.Name) || shopTitle.Contains(element.SubName))
                {
                    return element;
                }
            }
            return new Shop { Name = "Neatpažinta" };
        }
    }
}