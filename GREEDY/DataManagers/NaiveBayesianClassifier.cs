using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;

namespace GREEDY.DataManagers
{
    public class NaiveBayesianClassifier
    {
        public NaiveBayesianClassifier(List<ItemInfo> info)
        {
            Info = info;
            c = new Classifier(info);
        }
        private List<ItemInfo> Info { get; set; }
        private Classifier c;

        public string GetTopCategory(string test)
        {
            //var c = new Classifier(_trainCorpus);
            string max = "";
            double maxProb = 0;
            foreach (string element in Info.Select(x => x.Category).Distinct())
            {
                var res = c.IsInClassProbability(element, test);
                if (res > maxProb)
                {
                    max = element;
                    maxProb = res;
                }
            }
            return max;
        }
        
        public List<string> GetTopXCategories(string test, int numberOfCategories)
        {
            List<CategoryAndProb> cap = new List<CategoryAndProb>();
            CategoryAndProb n = new CategoryAndProb();
            List<string> categories = new List<string>();
            var c = new Classifier(Info);
            double res;
            cap.Clear();
            categories.Clear();
            foreach (string element in Info.Select(x => x.Category))
            {
                res = c.IsInClassProbability(element, test);
                n.Prob = res;
                n.Category = element;
                cap.Add(n);
                
            }
            List<string> teststr = new List<string>();
            foreach(CategoryAndProb thing in cap)
            {
                teststr.Add(thing.Category);
                teststr.Add(thing.Prob.ToString());

            }
            
           
            categories = cap.OrderByDescending(t => t.Prob).Select(o => o.Category).ToList();
            File.WriteAllLines("f:\\kazkas.txt", teststr);
            
            return categories;
        }
    }
    public class CategoryAndProb
    {
        public string Category;
        public double Prob;
    }
    public class ItemInfo
    {
        public ItemInfo(string category, string text)
        {
            Category = category;
            Text = text;
        }
        public string Category { get; set; }
        public string Text { get; set; }
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