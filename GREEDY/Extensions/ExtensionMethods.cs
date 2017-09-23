using System;
using System.Collections.Generic;

namespace GREEDY.Extensions
{
    public static class ExtensionMethods
    {
        public static bool IsAlmostEqual(this string thisStr, string str, float percentageToMatch)
        {
            var distance = LevenshteinDistance.Compute(str, thisStr);
            if (distance == 0) return true;                                                                  //Strings are completely equal
            if (Math.Max(str.Length, thisStr.Length) == 0) return false;
            float percentageMismatched = (float)((double)distance / Math.Max(str.Length, thisStr.Length) * 100); //Added double cast because float loses small values
            if (100 - percentageMismatched >= percentageToMatch * 100) return true;                          //Strings are almost equal
            else return false;                                                                               //Strings aren't equal enough
        }

        public static bool IsAlmostEqual(this string thisStr, List<string> list, float percentageToMatch)
        {
            string str = string.Empty;
            foreach (var line in list)
            {
                str += line;
            }
            return thisStr.IsAlmostEqual(str, percentageToMatch);
        }

        public static bool IsAlmostEqual(this List<string> thisList, List<string> list, float percentageToMatch)
        {
            string str1 = string.Empty, str2 = string.Empty;
            foreach (var line in thisList)
            {
                str1 += line;
            }
            foreach (var line in list)
            {
                str2 += line;
            }
            return str1.IsAlmostEqual(str2, percentageToMatch);
        }

        public static bool IsAlmostEqual(this List<string> thisList, string str, float percentageToMatch)
        {
            string str1 = string.Empty;
            foreach (var line in thisList)
            {
                str1 += line;
            }
            return str1.IsAlmostEqual(str, percentageToMatch);
        }
    }
}

