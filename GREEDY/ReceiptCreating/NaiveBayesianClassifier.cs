using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MoreLinq;
using GREEDY.Models;

namespace GREEDY.ReceiptCreatings
{
    public class NaiveBayesianClassifier
    {
        private static List<ItemInfo> Info;
        private static Classifier _classifier;

        public NaiveBayesianClassifier(List<ItemInfo> info)
        {
            Info = info;
            _classifier = new Classifier(info);
        }

        public (List<ItemInfo>, List<ItemInfo>) GetAllItemsWithCategories(List<ItemInfo> NewData)
        {
            double maxProb = 0;
            string maxCat = "";
            double prob = 0;

            var (asd1, asd2) = new Tuple<List<ItemInfo>, List<ItemInfo>>(Info, NewData);

            foreach (ItemInfo item in NewData)
            {
                foreach (string catg in Info.Select(x => x.Category).Distinct())
                {
                    prob = _classifier.IsInClassProbability(catg, item.Text);

                    if (prob > maxProb)
                    {
                        maxProb = prob;
                        maxCat = catg;
                    }
                }
                item.Category = maxCat;
                item.Prob = maxProb;
                maxProb = 0;
                maxCat = String.Empty;
            }
            //Add new data to all data
            //TODO: change this to one fixed testdata/trainingData fail
            Info = Info.Union(NewData).ToList();
            Info = Info.DistinctBy(o => o.Text).ToList();
            return (Info, NewData);
        }
    }

    public static class Helpers
    {
        public static List<String> ExtractFeatures(this String text)
        {
            return Regex.Replace(text, "\\p{P}+", "").Split(' ').ToList();
        }
    }

    //extract features and prepare for other classification 
    public class ClassInfo
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

    //training part
    //TODO: add sync/await or create separate class (like .dll or smth)
    public class Classifier
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

        private static double Calc(double dc, double d, List<String> q, double lc, ClassInfo classInfo, double v)
        {
            return Math.Log(dc / d) + q.Sum(x => Math.Log((classInfo.NumberOfOccurencesInTrainDocs(x) + 1) / (v + lc)));
        }
    }
}