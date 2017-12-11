using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
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

        public List<GraphData> GetDataForGraphs(string username, int? time)
        {
            DateTime endTime = DateTime.Now.AddSeconds(-time.Value);
            var items = context.Set<ItemDataModel>()
                .Where(x => x.Receipt.User.Username == username).Include(x => x.Receipt)
                .ToList();

            var graphDataList = items.Select(x => new GraphData()
            {
                ItemPrice = x.Price,
                Date = x.Receipt.UpdateDate,
                Time = x.Receipt.UpdateDate.ToString("HH:mm")
            }).ToList();
            return graphDataList;
        }

        public List<AverageStorePriceGraphData> GetDataForAverageStorePriceGraph(string username, int? time)
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
        }

        public List<MostBoughtItemsGraphData> GetDataForMostBoughtItemsGraph(string username, int? time)
        {
            DateTime endTime = DateTime.Now.AddSeconds(-time.Value);
            var items = context.Set<ItemDataModel>()
                .Where(x => x.Receipt.User.Username == username).ToList();

            var graphDataList = items
                .Where(n => !n.Category.Equals("nuolaida"))
                .GroupBy(n => n.Name)
                .Select(n => new MostBoughtItemsGraphData()
                {
                    ItemName =
                            (n.Key.Length > 15)
                                ? n.Key.Substring(0, 15)
                                : n.Key,
                    Count = n.Count()
                }
                )
                .OrderBy(n => n.Count).Reverse().Take(3).ToList();

            return graphDataList;
        }

        public List<ShopVisitCountGraphData> GetDataForShopVisitCountGraph(string username, int? time)
        {
            DateTime endTime = DateTime.Now.AddSeconds(-time.Value);
            var items = context.Set<ReceiptDataModel>()
                .Where(x => x.User.Username == username).ToList();

            var graphDataList = items
                .GroupBy(n => n.Shop.HasValue() ? n.Shop.Name : null)
                .Select(n => new ShopVisitCountGraphData()
                {
                    ShopName = n.Key,
                    Visits = n.Count()
                }
                )
                .OrderBy(n => n.ShopName).ToList();

            return graphDataList;
        }

        public List<ShopItemCountGraphData> GetDataForShopItemCountGraph(string username, int? time)
        {
            DateTime endTime = DateTime.Now.AddSeconds(-time.Value);
            var items = context.Set<ItemDataModel>()
                .Where(x => x.Receipt.User.Username == username).ToList();

            var graphDataList = items
                .Where(n => !n.Category.Equals("nuolaida"))
                .GroupBy(n => n.Receipt.Shop.HasValue() ? n.Receipt.Shop.Name : null)
                .Select(n => new ShopItemCountGraphData()
                {
                    ShopName = n.Key,
                    ItemsCount = n.Count()
                }
                )
                .OrderBy(n => n.ShopName).ToList();

            return graphDataList;
        }

        public List<WeekVisitsGraphData> GetDataForWeekVisitsGraph(string username, int? time)
        {
            DateTime endTime = DateTime.Now.AddSeconds(-time.Value);
            var items = context.Set<ReceiptDataModel>()
                .Where(x => x.User.Username == username).ToList();

            var graphDataList = items
                .GroupBy(n => n.ReceiptDate.HasValue ? n.ReceiptDate.Value.DayOfWeek
                    : n.UpdateDate.DayOfWeek)
                .Select(n => new WeekVisitsGraphData()
                {
                    WeekDay = n.Key,
                    WeekDayString = n.Key.ToString(),
                    VisitsCount = n.Count()
                }
                )
                .OrderBy(n => n.WeekDay).ToList();

            return graphDataList;
        }

        void IDisposable.Dispose()
        {
            context.Dispose();
        }
    }
}
