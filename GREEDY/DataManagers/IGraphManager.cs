using System.Collections.Generic;
using GREEDY.Models;

namespace GREEDY.DataManagers
{
    public interface IGraphManager
    {
        List<GraphData> GetDataForGraphs(string username, int? time);
    }
}
