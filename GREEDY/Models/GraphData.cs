using System;
using System.Collections.Generic;

namespace GREEDY.Models
{
    public struct GraphData<T> //where T : int, decimal, long...
    {
        public string label;
        public T value;
    }

    public class FullGraphData
    {
        public List<GraphData<int>> CategoriesData { get; set; }
    }
}
