using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GREEDY.Data;
using GREEDY.Models;

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
                .Where(x => x.Receipt.User.Username == username).ToList();

            var graphDataList = items.Select(x => new GraphData()
            {
                ItemPrice = x.Price,
                Date = x.Receipt.Date,
                Time = x.Receipt.Date.ToString("HH:mm")
            }).ToList();
            return graphDataList;
        }

        void IDisposable.Dispose()
        {
            context.Dispose();
        }
    }
}
