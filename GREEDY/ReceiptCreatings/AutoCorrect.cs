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
        private static LevenshteinDistance LevenshteinDistance => new LevenshteinDistance();

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
                string name = String.Empty;
                string suggection = String.Empty;
                double ratio;
                for (int i = 0; i < itemList.Count; i++)
                {
                    foreach (string word in itemList[i].Name.Split(' '))
                    {
                        if (!hunspell.Spell(word))
                        {
                            suggection = hunspell.Suggest(word).First();
                            ratio = ((double)LevenshteinDistance.Compute(suggection, word)) / (Math.Max(suggection.Length, word.Length));

                            if (ratio < 0.3)
                            {
                                name += hunspell.Suggest(word).First() + " ";
                            }
                            else
                            {
                                name += word + " ";
                            }
                        }
                        else
                        {
                            name += word + " ";
                        }
                    }
                    itemList[i].Name = name;
                    name = String.Empty;
                }
            }
            return itemList;
        }
    }
}