using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GREEDY.Data;
using GREEDY.Models;

using GREEDY.Extensions;

namespace GREEDY.DataManagers
{
    public class GraphManager : IGraphManager, IDisposable
    {
        private DbContext context;
        public GraphManager(DbContext context)
        {
            this.context = context;
        }

        public FullGraphData GetDataForGraphs(string username, int time)
        {
            DateTime endTime;
            if (time == 0)
            {
                endTime = DateTime.MinValue;
            }
            else
            {
                endTime = DateTime.Now.AddHours(-time);
            }
            var fullGraphData = new FullGraphData();

            var receipts = context.Set<ReceiptDataModel>().Include(x => x.Shop).Include(x => x.Items)
                .Where(x => x.User.Username == username &&
                x.ReceiptDate.HasValue ? x.ReceiptDate.Value > endTime : x.UpdateDate > endTime)
                .ToList();

            var items = receipts.SelectMany(x => x.Items).ToList();

            fullGraphData.CategoriesData = items.Where(x => !x.Category.Equals("nuolaida"))
                .GroupBy(x => x.Category)
                .Select(x => new GraphData(x.Key.ToString(), x.Count())).ToList()
                .OverflowHandler(Environments.AppConfig.ShowItemsInGraphs);


            fullGraphData.MoneySpentInShops = receipts.GroupBy(x => x.Shop != null ? x.Shop.Name : null).Select(x =>
                new GraphData(x.Key, x.Sum(y => y.Total))).ToList();

            //TODO: figure out why this doesn't work
            var undefinedShop = fullGraphData.MoneySpentInShops.FirstOrDefault(x => x.label == null);
            if (undefinedShop.value != 0)
            {
                undefinedShop.label = "Other";
            }

            //TODO: fix this to return empty values for non-existing week days
            //TODO: should make this an average, not the total count
            var receiptGroupedByDayOfWeek = receipts.GroupBy(x => x.ReceiptDate.HasValue ? x.ReceiptDate.Value.DayOfWeek
                    : x.UpdateDate.DayOfWeek).OrderBy(x => x.Key);
            fullGraphData.WeekShoppingCount = receiptGroupedByDayOfWeek.Select(x => new GraphData(x.Key.ToString(), x.Count())).ToList();
            //TODO: should make this an average, not the total count
            fullGraphData.WeekShoppingPrice = receiptGroupedByDayOfWeek.Select(x => new GraphData(x.Key.ToString(), x.Sum(y => y.Total))).ToList();

            return fullGraphData;
        }

        //TODO: keeping this for references, might need to implement a similar graph
        /*public List<AverageStorePriceGraphData> GetDataForAverageStorePriceGraph(string username, int? time)
        {
            DateTime endTime = DateTime.Now.AddSeconds(-time.Value);
            var items = context.Set<ItemDataModel>()
                .Where(x => x.Receipt.User.Username == username).ToList();

            var graphDataList = items
                .GroupBy(x => x.Receipt.Shop.HasValue() ? x.Receipt.Shop.Name : null)
                .Select(g => new AverageStorePriceGraphData()
                {
                    ShopName = g.Key,
                    AveragePrice = decimal.Round(g.Average(x => x.Price), 2, MidpointRounding.AwayFromZero)
                }).OrderBy(n => n.ShopName).ToList();

            return graphDataList;
        }*/

        void IDisposable.Dispose()
        {
            context.Dispose();
        }
    }
}
