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
            if (distance == 0) return true;                                                                  //Strings are completely equal
            if (Math.Max(str.Length, str.Length) == 0) return false;
            float percentageMismatched = (float)((double)distance / Math.Max(str.Length, str.Length) * 100); //Added double cast because float loses small values
            if (100 - percentageMismatched >= percentageToMatch * 100) return true;                          //Strings are almost equal
            else return false;                                                                               //Strings aren't equal enough
        }
    }
}

