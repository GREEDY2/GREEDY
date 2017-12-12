﻿using System;
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

            fullGraphData.CategoriesData = items.Where(x => !x.Category.CategoryName.Equals("nuolaida"))
                .GroupBy(x => x.Category.CategoryName)
                .Select(x => new GraphData(x.Key.ToString(), x.Count())).ToList()
                .OverflowHandler(Environments.AppConfig.ShowItemsInGraphs);


            fullGraphData.MoneySpentInShops = receipts.GroupBy(x => x.Shop != null ? x.Shop.Name : null).Select(x =>
                new GraphData(x.Key, x.Sum(y => y.Total))).ToList();

            var undefinedShop = fullGraphData.MoneySpentInShops.FirstOrDefault(x => x.label == null);
            if (undefinedShop.value != 0)
            {
                fullGraphData.MoneySpentInShops[fullGraphData.MoneySpentInShops.IndexOf(undefinedShop)] = new GraphData {
                    label = "Other",
                    value = fullGraphData.MoneySpentInShops[fullGraphData.MoneySpentInShops.IndexOf(undefinedShop)].value };
            }

        var receiptGroupedByDayOfWeek = receipts.GroupBy(x => x.ReceiptDate.HasValue ? x.ReceiptDate.Value.DayOfWeek
                    : x.UpdateDate.DayOfWeek).OrderBy(x => x.Key);

            var WeekShopping = new List<GraphData>
            {
                new GraphData { label = DayOfWeek.Monday.ToString(), value = 0},
                new GraphData { label = DayOfWeek.Saturday.ToString(), value = 0},
                new GraphData { label = DayOfWeek.Sunday.ToString(), value = 0},
                new GraphData { label = DayOfWeek.Thursday.ToString(), value = 0},
                new GraphData { label = DayOfWeek.Tuesday.ToString(), value = 0},
                new GraphData { label = DayOfWeek.Wednesday.ToString(), value = 0},
                new GraphData { label = DayOfWeek.Friday.ToString(), value = 0},
            };

            var Count = receiptGroupedByDayOfWeek.Select(x => new GraphData(x.Key.ToString(), x.Count())).ToList();
            var Price = receiptGroupedByDayOfWeek.Select(x => new GraphData(x.Key.ToString(), x.Sum(y => y.Total))).ToList();

            //It is slow in general, but it could be enaught fast for 7^2 items
            var products = WeekShopping.Where(p => !Count.Any(y => p.label == y.label)).ToList();
            fullGraphData.WeekShoppingCount = Count.Concat(products).OrderBy(x => x.label).ToList();

            products = WeekShopping.Where(p => !Price.Any(y => p.label == y.label)).ToList();
            fullGraphData.WeekShoppingPrice = Count.Concat(products).OrderBy(x => x.label).ToList();

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
