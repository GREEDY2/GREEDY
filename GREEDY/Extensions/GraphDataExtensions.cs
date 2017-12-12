using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GREEDY.Models;

namespace GREEDY.Extensions
{
    public static class GraphDataExtensions
    {
        /*public static List<GraphData<T>> GetLinesOfText<T>(this List<GraphData<T>> list, int itemsToShow)
        {
            //TODO: figure how to do constraints on structs or rewrite GraphData structure
            var orderedList = list.OrderBy(x => x.value);
            var overflowData = orderedList.Skip(itemsToShow).Sum(x => x.value);

            orderedList.Take(itemsToShow).Concat(
                );

            return list;
        }*/
        public static List<GraphData> OverflowHandler (this List<GraphData> list, int itemsToShow)
        {
            if (list.Count <= itemsToShow)
            {
                return list;
            }
            var orderedList = list.OrderBy(x => x.value);
            var overflowSum = orderedList.Skip(itemsToShow).Sum(x => x.value);
            var newList = orderedList.Take(itemsToShow).ToList();
            
            return newList;
        }
    }
}
