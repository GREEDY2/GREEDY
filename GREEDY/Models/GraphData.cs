using System;
using System.Collections.Generic;

namespace GREEDY.Models
{
    //TODO: show this to Aseris, and discuss wheter there is a way to do this
    /*public abstract class INumber
    {
        public static INumber operator +(INumber other) { return null; }
    }
    public class Integer : INumber
    {
        private int _underlying;
        public Integer(int a)
        {
            _underlying = a;
        }
        public static implicit operator Integer(int a)
        {
            return new Integer(a);
        }
        public static Integer operator +(Integer a, Integer b)
        {
            return new Integer(a._underlying+b._underlying);
        }
    }

    public struct GraphData<T> where T :  INumber//where T : int, decimal, long...
    {
        public string label;
        public T value;
    }*/

    public struct GraphData
    {
        public GraphData(string label, decimal value)
        {
            this.label = label;
            this.value = value;
        }

        public string label;
        public decimal value;
    }

    public class FullGraphData
    {
        public List<GraphData> CategoriesData { get; set; }
        public List<GraphData> MoneySpentInShops { get; set; }
        public List<GraphData> WeekShoppingCount { get; set; }
        public List<GraphData> WeekShoppingPrice { get; set; }
    }
}
