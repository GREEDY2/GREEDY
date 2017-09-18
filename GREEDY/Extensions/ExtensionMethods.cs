using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static bool IsAlmostEqual(this List<string> thisList, List<string> list, float percentageToMatch)
        {
            int distance = 0;
            int strLength1 = 0, strLength2 = 0;
            for (int i = 0; i < thisList.Count && i < list.Count; i++)
            {
                distance += LevenshteinDistance.Compute(thisList[i], list[i]);
                strLength1 += thisList[i].Length;
                strLength2 += list[i].Length;
            }
            if (distance == 0) return true;                                                                  //Strings are completely equal
            if (Math.Max(strLength1, strLength2) == 0) return false;
            float percentageMismatched = (float)((double)distance / Math.Max(strLength1, strLength2) * 100); //Added double cast because float loses small values
            if (100 - percentageMismatched >= percentageToMatch * 100) return true;                          //Strings are almost equal
            else return false;                                                                               //Strings aren't equal enough
        }
    }
}

