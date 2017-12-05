using Geocoding;
using Geocoding.Google;
using GREEDY.Data;
using GREEDY.DataManagers;
using GREEDY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GREEDY.ReceiptCreatings
{
    public class ShopDetection : IShopDetection
    {
        private readonly IShopManager _shops;

        public ShopDetection()
        {
            _shops = new ShopManager(new DataBaseModel());
        }

        public Shop GetShopFromData(List<string> linesOfText)
        {
            var FirstLine = String.Join(String.Empty, linesOfText);
            FirstLine = Regex.Replace(FirstLine, @"\r\r\n", " ");
            var Allshops = _shops.GetExistingShops();
            var ShopName = GetShopName(FirstLine, Allshops);
            /*if (ShopName == "Neatpažinta")
            {
                //(var address, var location) = GetAddressAndLocation(FirstLine.Split(' ')).Result;
                return new Shop { Name = "Neatpažinta" };
            }
            else
            {
                return GetShopWithLocation(FirstLine, Allshops
                    .Where(x => x.Name == ShopName || x.SubName == ShopName).ToList());
            }*/
            return GetShopWithLocation(FirstLine, Allshops
                    .Where(x => x.Name == ShopName || x.SubName == ShopName).ToList());
        }

        public Shop GetShopWithLocation(string FirstLine, List<Shop> shops)
        {
            foreach (Shop element in shops)
            {
                if (FirstLine.ToUpper().Contains(element.Address.ToString().ToUpper()))
                {
                    return element;
                }
            }
            return new Shop
            {
                Name = shops.First().Name,
                SubName = shops.First().SubName,
                Location = new Location(0, 0),
                Address = "Neatpažinta"
            };
        }

        //need to improve detection with Levenshtein distance
        public async Task<(string, Location)> GetAddressAndLocation(string[] FirstLine)
        {
            IGeocoder geocoder = new GoogleGeocoder() { ApiKey = Environments.AppConfig.GoogleMapsGeocodingAPIKey };
            for (int i = 1; i < FirstLine.Count() - 2; i++)
            {
                var text = FirstLine[i - 1] + FirstLine[i] + FirstLine[i + 1] + FirstLine[i + 2];
                IEnumerable<Address> addresses = await geocoder.GeocodeAsync(text);
                if (addresses.Count() != 0)
                {
                    string address = addresses.First().FormattedAddress;
                    Location location = addresses.First().Coordinates;
                    return (address, location);
                }
            }
            return (null, null);
        }

        public string GetShopName(string FirstLines, List<Shop> shops)
        {
            var names = shops.Select(x => x.Name).OfType<string>().Distinct()
                .Concat(shops.Select(x => x.SubName).OfType<string>().Distinct()).ToList();

            foreach (string element in names)
            {
                if (FirstLines.IndexOf(element, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    return element;
                }
            }
            return "Neatpažinta";
        }
    }
}