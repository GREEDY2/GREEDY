using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GREEDY.Extensions
{
    public static class ExtensionMethods
    {
        public static bool IsAlmostEqual(this string str2, string str, float percentageToMatch)
        {
            var distance = LevenshteinDistance.Compute(str, str2);
            if (distance == 0) return true;                                 //Strings are completely equal
            float percentageMismatched = distance / Math.Max(str.Length, str.Length);
            if (1 - percentageMismatched >= percentageToMatch) return true; //Strings are almost equal
            else return false;                                              //Strings aren't equal enough
        }
    }
}

