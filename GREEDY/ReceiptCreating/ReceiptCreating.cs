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
using Geocoding.Google;
using Geocoding;
using System.Threading.Tasks;

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
            var address = GetAddress(linesOfText.Take(4)).Result;
            var shop = GetShopFromData(linesOfText.Take(4), address);

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

        public Shop GetShopFromData(IEnumerable<string> linesOfText, Location address)
        {
            var shopTitle = String.Join(String.Empty, linesOfText);
            var shops = _shops.GetExistingShop();

            foreach (Shop element in shops)
            {
                //if shopTitle is empty = 0
                if (shopTitle.IndexOf(element.Name, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    return new Shop
                    {
                        Name = element.Name,
                        Location = address,
                        SubName = element.SubName
                    };
                }
                else if (element.SubName != null && shopTitle.IndexOf(element.SubName, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    return new Shop
                    {
                        Name = element.Name,
                        Location = address,
                        SubName = element.SubName
                    };
                }
            }
            return new Shop { Name = "Neatpažinta", Location = address };
        }

        public async Task<Location> GetAddress(IEnumerable<string> linesOfText)
        {
            IGeocoder geocoder = new GoogleGeocoder() { ApiKey = Environments.AppConfig.GoogleMapsGeocodingAPIKey };
            IEnumerable<Address> addresses = await geocoder.GeocodeAsync("Ozo g. 16, Vilnius");
            var coordinates = addresses.First().Coordinates;
            Console.WriteLine("Formatted: " + addresses.First().FormattedAddress); //Formatted: 1600 Pennsylvania Ave SE, Washington, DC 20003, USA
            Console.WriteLine("Coordinates: " + coordinates.Latitude + ", " + coordinates.Longitude); //Coordinates: 38.8791981, -76.9818437

            return coordinates;
        }
    }
}