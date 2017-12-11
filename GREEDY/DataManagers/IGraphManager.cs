using System.Collections.Generic;
using GREEDY.Models;

namespace GREEDY.DataManagers
{
    public interface IGraphManager
    {
        FullGraphData GetDataForGraphs(string username, int time);
        List<AverageStorePriceGraphData> GetDataForAverageStorePriceGraph(string username, int? time);
        List<MostBoughtItemsGraphData> GetDataForMostBoughtItemsGraph(string username, int? time);
        List<ShopVisitCountGraphData> GetDataForShopVisitCountGraph(string username, int? time);
        List<ShopItemCountGraphData> GetDataForShopItemCountGraph(string username, int? time);
        List<WeekVisitsGraphData> GetDataForWeekVisitsGraph(string username, int? time);
    }
}
