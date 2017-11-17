//using System;
//using System.Collections.Generic;
//using GREEDY.Models;
//using NHunspell;

//namespace GREEDY.ReceiptCreatings
//{
//    class AutoCorrect
//    {
//        public List<Item> ItemsAutoCorrect(List<Item> itemList)
//        {
//            //prideti kalba
//            using (SpellEngine engine = new SpellEngine())
//            {
//                LanguageConfig enConfig = new LanguageConfig
//                {
//                    LanguageCode = "en",
//                    HunspellAffFile = "en_us.aff",
//                    HunspellDictFile = "en_us.dic",
//                    HunspellKey = "",
//                    HyphenDictFile = "hyph_en_us.dic",
//                    MyThesDatFile = "th_en_us_new.dat"
//                };
//                engine.AddLanguage(enConfig);

//                bool correct = engine["en"].Spell("Recommendation");
//                Console.WriteLine("Recommendation is spelled " + (correct ? "correct" : "not correct"));

//            }

//            using (Hunspell hunspell = new Hunspell("en_us.aff", "en_us.dic"))
//            {
//                Console.WriteLine("Check if the word 'Recommendation' is spelled correct");
//                bool correct = hunspell.Spell("Recommendation");
//                Console.WriteLine("Recommendation is spelled " + (correct ? "correct" : "not correct"));

//                Console.WriteLine("");
//                Console.WriteLine("Make suggestions for the misspelled word 'Recommendatio'");
//                List<string> suggestions = hunspell.Suggest("Recommendatio");
//                Console.WriteLine("There are " + suggestions.Count.ToString() + " suggestions");
//                foreach (string suggestion in suggestions)
//                {
//                    Console.WriteLine("Suggestion is: " + suggestion);
//                }
//            }
//            return itemList;
//        }
//    }
//}


////https://sourceforge.net/p/nhunspell/code/ci/default/tree/NHunspellSamples/CSharpConsoleSamples/Program.cs#l68

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

            // Important: Due to the fact Hunspell will use unmanaged memory you have to serve the IDisposable pattern
            // In this block of code this is be done by a using block. But you can also call hunspell.Dispose()

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
                bool correct;
                List<string> suggestions;
                for (int i = 0; i < itemList.Count; i++)
                {
                    foreach (string word in itemList[i].Name.Split(' '))
                    {
                        correct = hunspell.Spell(word);
                        Console.WriteLine(word + " is spelled " + (correct ? "correct" : "not correct"));

                        if (!correct)
                        {
                            Console.WriteLine("Make suggestions for the misspelled word ", word);
                            suggestions = hunspell.Suggest(word);
                            Console.WriteLine("There are " + suggestions.Count + " suggestions");
                            foreach (string suggestion in suggestions)
                            {
                                Console.WriteLine("Suggestion is: " + suggestion);
                            }
                        }
                    }
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