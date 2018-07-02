using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Geocoding;
using Geocoding.Google;
using GREEDY.Data;
using GREEDY.DataManagers;
using GREEDY.Models;

namespace GREEDY.ReceiptCreating
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
            var firstLine = string.Join(string.Empty, linesOfText);
            firstLine = Regex.Replace(firstLine, @"\r\r\n", " ");
            var allshops = _shops.GetExistingShops();
            var shopName = GetShopName(firstLine, allshops);
            if (shopName == null)
                return null;
            return GetShopWithLocation(firstLine, allshops
                .Where(x => x.Name == shopName || x.SubName == shopName).ToList());
        }

        public Shop GetShopWithLocation(string firstLine, List<Shop> shops)
        {
            foreach (var element in shops)
                if (firstLine.ToUpper().Contains(element.Address.ToUpper()))
                    return element;
            return new Shop
            {
                Name = shops.First().Name,
                SubName = shops.First().SubName,
                Location = new Location(0, 0)
            };
        }

        //need to improve detection with Levenshtein distance
        public async Task<(string, Location)> GetAddressAndLocation(string[] firstLine)
        {
            IGeocoder geocoder = new GoogleGeocoder {ApiKey = Environments.AppConfig.GoogleMapsGeocodingAPIKey};
            for (var i = 1; i < firstLine.Count() - 2; i++)
            {
                var text = firstLine[i - 1] + firstLine[i] + firstLine[i + 1] + firstLine[i + 2];
                var addresses = await geocoder.GeocodeAsync(text);
                var addressesList = addresses.ToList();
                if (!addressesList.Any()) continue;
                var address = addressesList.First().FormattedAddress;
                var location = addressesList.First().Coordinates;
                return (address, location);
            }

            return (null, new Location(0, 0));
        }

        public string GetShopName(string firstLines, List<Shop> shops)
        {
            var names = shops?.Select(x => x.Name).Distinct()
                .Concat(shops.Select(x => x.SubName).Distinct()).ToList();

            if (names == null) return null;
            foreach (var element in names)
                if (firstLines.IndexOf(element, StringComparison.OrdinalIgnoreCase) > 0)
                    return element;
            return null;
        }
    }
}