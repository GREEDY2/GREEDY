using System;
using System.Collections.Generic;
using GREEDY.Models;
using NHunspell;

namespace GREEDY.ReceiptCreatings
{
    class AutoCorrect
    {
        public List<Item> ItemsAutoCorrect(List<Item> itemList)
        {
            //prideti kalba
            using (SpellEngine engine = new SpellEngine())
            {
                LanguageConfig enConfig = new LanguageConfig
                {
                    LanguageCode = "en",
                    HunspellAffFile = "en_us.aff",
                    HunspellDictFile = "en_us.dic",
                    HunspellKey = "",
                    HyphenDictFile = "hyph_en_us.dic",
                    MyThesDatFile = "th_en_us_new.dat"
                };
                engine.AddLanguage(enConfig);

                bool correct = engine["en"].Spell("Recommendation");
                Console.WriteLine("Recommendation is spelled " + (correct ? "correct" : "not correct"));

            }

            using (Hunspell hunspell = new Hunspell("en_us.aff", "en_us.dic"))
            {
                Console.WriteLine("Check if the word 'Recommendation' is spelled correct");
                bool correct = hunspell.Spell("Recommendation");
                Console.WriteLine("Recommendation is spelled " + (correct ? "correct" : "not correct"));

                Console.WriteLine("");
                Console.WriteLine("Make suggestions for the misspelled word 'Recommendatio'");
                List<string> suggestions = hunspell.Suggest("Recommendatio");
                Console.WriteLine("There are " + suggestions.Count.ToString() + " suggestions");
                foreach (string suggestion in suggestions)
                {
                    Console.WriteLine("Suggestion is: " + suggestion);
                }
            }
            return itemList;
        }
    }
}


//https://sourceforge.net/p/nhunspell/code/ci/default/tree/NHunspellSamples/CSharpConsoleSamples/Program.cs#l68