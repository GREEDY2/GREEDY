using GREEDY.Models;

namespace GREEDY.DataManagers
{
    public interface IGraphManager
    {
        FullGraphData GetDataForGraphs(string username, int time);
    }
}