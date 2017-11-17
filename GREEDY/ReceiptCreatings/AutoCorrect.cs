using System;
using System.Collections.Generic;
using GREEDY.Models;
using NHunspell;
using System.IO;
using System.Linq;

namespace GREEDY.ReceiptCreatings
{
    class AutoCorrect
    {
        public List<Item> ItemsAutoCorrect(List<Item> itemList)
        {
            using (Hunspell hunspell = new Hunspell("lt_LT.aff", "lt_LT.dic"))
            {
                //preparing specific data
                string[] lines = File.ReadAllLines("Brands.txt");
                foreach (var line in lines)
                {
                    hunspell.Add(line);
                }

                lines = File.ReadAllLines("CustomWords-en_US.txt");
                foreach (var line in lines)
                {
                    hunspell.Add(line);
                }

                //Main autoCorrect
                string Name = String.Empty;
                for (int i = 0; i < itemList.Count; i++)
                {
                    foreach (string word in itemList[i].Name.Split(' '))
                    {
                        if (!hunspell.Spell(word))
                        {
                            Name += hunspell.Suggest(word).First() + " ";
                            Console.WriteLine(Name);
                        }
                        else
                        {
                            Name += word + " ";
                            Console.WriteLine(Name);
                        }
                    }
                    itemList[i].Name = Name;
                    Name = String.Empty;
                }
            }
            return itemList;
        }
    }
}


////https://sourceforge.net/p/nhunspell/code/ci/default/tree/NHunspellSamples/CSharpConsoleSamples/Program.cs#l68


///// <summary>
///// This class implements string comparison algorithm
///// based on character pair similarity
///// Source: http://www.catalysoft.com/articles/StrikeAMatch.html
///// </summary>
//public class SimilarityTool
//{
//    /// <summary>
//    /// Compares the two strings based on letter pair matches
//    /// </summary>
//    /// <param name="str1"></param>
//    /// <param name="str2"></param>
//    /// <returns>The percentage match from 0.0 to 1.0 where 1.0 is 100%</returns>
//    public double CompareStrings(string str1, string str2)
//    {
//        List<string> pairs1 = WordLetterPairs(str1.ToUpper());
//        List<string> pairs2 = WordLetterPairs(str2.ToUpper());

//        int intersection = 0;
//        int union = pairs1.Count + pairs2.Count;

//        for (int i = 0; i < pairs1.Count; i++)
//        {
//            for (int j = 0; j < pairs2.Count; j++)
//            {
//                if (pairs1[i] == pairs2[j])
//                {
//                    intersection++;
//                    pairs2.RemoveAt(j);//Must remove the match to prevent "GGGG" from appearing to match "GG" with 100% success

//                    break;
//                }
//            }
//        }

//        return (2.0 * intersection) / union;
//    }

//    /// <summary>
//    /// Gets all letter pairs for each
//    /// individual word in the string
//    /// </summary>
//    /// <param name="str"></param>
//    /// <returns></returns>
//    private List<string> WordLetterPairs(string str)
//    {
//        List<string> AllPairs = new List<string>();

//        // Tokenize the string and put the tokens/words into an array
//        string[] Words = Regex.Split(str, @"\s");

//        // For each word
//        for (int w = 0; w < Words.Length; w++)
//        {
//            if (!string.IsNullOrEmpty(Words[w]))
//            {
//                // Find the pairs of characters
//                String[] PairsInWord = LetterPairs(Words[w]);

//                for (int p = 0; p < PairsInWord.Length; p++)
//                {
//                    AllPairs.Add(PairsInWord[p]);
//                }
//            }
//        }

//        return AllPairs;
//    }

//    /// <summary>
//    /// Generates an array containing every 
//    /// two consecutive letters in the input string
//    /// </summary>
//    /// <param name="str"></param>
//    /// <returns></returns>
//    private string[] LetterPairs(string str)
//    {
//        int numPairs = str.Length - 1;

//        string[] pairs = new string[numPairs];

//        for (int i = 0; i < numPairs; i++)
//        {
//            pairs[i] = str.Substring(i, 2);
//        }

//        return pairs;
//    }
//}