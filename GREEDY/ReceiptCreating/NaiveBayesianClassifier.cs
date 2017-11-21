using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
using MoreLinq;

namespace GREEDY.ReceiptCreating
{
    public class NaiveBayesianClassifier
    {
        public NaiveBayesianClassifier(List<ItemInfo> info)
        {
            Info = info;
            c = new Classifier(info);
        }
        public List<ItemInfo> Info { get; set; }
        private Classifier c;

        /*public string GetTopCategory(string itemName)
        {
            string max = "";
            double maxProb = 0;
            foreach (string element in Info.Select(x => x.Category).Distinct())
            {
                var res = c.IsInClassProbability(element, itemName);
                if (res > maxProb)
                {
                    max = element;
                    maxProb = res;
                }
            }
            return max;
        }*/

        /*public List<string> GetAllCategoriesSorted(string itemName)
        {
            List<string> categories = new List<string>();
            List<CategoryAndProb> cap = new List<CategoryAndProb>();
            foreach (string element in Info.Select(x => x.Category).Distinct())
            {
                var res = c.IsInClassProbability(element, itemName);
                cap.Add(new CategoryAndProb(element, res));
            }

            cap = cap.OrderByDescending(x => x.Prob).ToList();
            foreach (CategoryAndProb element in cap)
                categories.Add(element.Category);
            return categories;
        }*/

        public List<ItemInfo> GetAllItemsWithCategories(List<ItemInfo> NewData)
        {
            double maxProb = 0;
            string maxCat = "";
            double res = 0;
            foreach (ItemInfo item in NewData)
            {
                foreach (string catg in Info.Select(x => x.Category).Distinct())
                {
                    res = c.IsInClassProbability(catg, item.Text);

                    if (res > maxProb)
                    {
                        maxProb = res;
                        maxCat = catg;
                    }
                }
                item.Category = maxCat;
                item.Prob = maxProb;
                maxProb = 0;
                maxCat = "";
            }
            Info = Info.Union(NewData).ToList();
            Info = Info.DistinctBy(o => o.Text).ToList();
            return NewData;
        } 

    }
    
    public class ItemInfo
    {
        public ItemInfo(string category, string text, double prob)
        {
            Category = category;
            Text = text;
        }
        public string Category { get; set; }
        public string Text { get; set; }
        public double Prob { get; set; }

        public string println()
        {
            return Category + Text + Prob.ToString();
        }
    }

    public static class Helpers
    {
        public static List<String> ExtractFeatures(this String text)
        {
            return Regex.Replace(text, "\\p{P}+", "").Split(' ').ToList();
        }
    }

    class ClassInfo
    {
        public ClassInfo(string name, List<String> trainDocs)
        {
            Name = name;
            var features = trainDocs.SelectMany(x => x.ExtractFeatures());
            WordsCount = features.Count();
            WordCount =
                features.GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());
            NumberOfDocs = trainDocs.Count;
        }
        public string Name { get; set; }
        public int WordsCount { get; set; }
        public Dictionary<string, int> WordCount { get; set; }
        public int NumberOfDocs { get; set; }
        public int NumberOfOccurencesInTrainDocs(String word)
        {
            if (WordCount.Keys.Contains(word)) return WordCount[word];
            return 0;
        }
    }

    class Classifier
    {
        List<ClassInfo> _classes;
        int _countOfDocs;
        int _uniqWordsCount;
        public Classifier(List<ItemInfo> train)
        {
            _classes = train.GroupBy(x => x.Category).Select(g => new ClassInfo(g.Key, g.Select(x => x.Text).ToList())).ToList();
            _countOfDocs = train.Count;
            _uniqWordsCount = train.SelectMany(x => x.Text.Split(' ')).GroupBy(x => x).Count();
        }

        public double IsInClassProbability(string className, string text)
        {
            var words = text.ExtractFeatures();
            var classResults = _classes
                .Select(x => new
                {
                    Result = Math.Pow(Math.E, Calc(x.NumberOfDocs, _countOfDocs, words, x.WordsCount, x, _uniqWordsCount)),
                    ClassName = x.Name
                });


            return classResults.Single(x => x.ClassName == className).Result / classResults.Sum(x => x.Result);
        }

        private static double Calc(double dc, double d, List<String> q, double lc, ClassInfo @class, double v)
        {
            return Math.Log(dc / d) + q.Sum(x => Math.Log((@class.NumberOfOccurencesInTrainDocs(x) + 1) / (v + lc)));
        }
    }


}