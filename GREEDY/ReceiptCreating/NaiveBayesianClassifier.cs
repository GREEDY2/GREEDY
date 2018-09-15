using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GREEDY.Models;

namespace GREEDY.ReceiptCreating
{
    //extract features and prepare for other classification (MODEL)
    public class ClassInfo
    {
        public ClassInfo(string name, ICollection<string> trainDocs)
        {
            Name = name;
            var features = trainDocs.SelectMany(x => x.ExtractFeatures());
            var featuresList = features.ToList();
            WordsCount = featuresList.Count();
            WordCount = featuresList.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            NumberOfDocs = trainDocs.Count;
        }

        public string Name { get; set; }
        public int WordsCount { get; set; }
        public Dictionary<string, int> WordCount { get; set; }
        public int NumberOfDocs { get; set; }

        public int NumberOfOccurencesInTrainDocs(string word) => WordCount.Keys.Contains(word) ? WordCount[word] : 0;
    }

    public class NaiveBayesianClassifier
    {
        private List<ClassInfo> _classes;
        private int _countOfDocs;
        private int _uniqWordsCount;

        public NaiveBayesianClassifier(List<ItemClassificationModels> _trainingData)
        {
            ClassifierTraining(_trainingData);
        }

        public List<ItemClassificationModels> GetAllItemsWithCategories(List<ItemClassificationModels> NewData)
        {
            double maxProb = 0;
            var maxCat = "";

            foreach (var item in NewData)
                if (item.Category == string.Empty)
                {
                    foreach (var category in _classes.Select(x => x.Name).Distinct())
                    {
                        var prob = IsInClassProbability(category, item.Text);

                        if (!(prob > maxProb)) continue;
                        maxProb = prob;
                        maxCat = category;
                    }

                    if (maxProb < 0.15)
                    {
                        item.Category = "food";
                        item.Prob = 1;
                    }
                    else
                    {
                        item.Category = maxCat;
                        item.Prob = maxProb;
                    }

                    maxProb = 0;
                    maxCat = string.Empty;
                }

            return NewData;
        }

        public double IsInClassProbability(string category, string text)
        {
            var words = text.ExtractFeatures();
            var classResults = _classes
                .Select(x => new
                {
                    Result = Math.Pow(Math.E,
                        Calc(x.NumberOfDocs, _countOfDocs, words, x.WordsCount, x, _uniqWordsCount)),
                    ClassName = x.Name
                });
            var classResultList = classResults.ToList();
            return classResultList.Single(x => x.ClassName == category).Result / classResultList.Sum(x => x.Result);
        }

        private static double Calc(double dc, double d, List<string> q, double lc, ClassInfo classInfo, double v) => 
            Math.Log(dc / d) + q.Sum(x => Math.Log((classInfo.NumberOfOccurencesInTrainDocs(x) + 1) / (v + lc)));

        private void ClassifierTraining(ICollection<ItemClassificationModels> train)
        {
            _classes = train.GroupBy(x => x.Category)
                .Select(g => new ClassInfo(g.Key.ToLower(), g.Select(x => x.Text.ToLower()).ToList())).ToList();
            _countOfDocs = train.Count;
            _uniqWordsCount = train.SelectMany(x => x.Text.Split(' ')).GroupBy(x => x).Count();
        }
    }

    public static class Helpers
    {
        public static List<string> ExtractFeatures(this string text) => 
            Regex.Matches(text, "\\p{L}{4,}").Cast<Match>().Select(match => match.Value.ToLower()).ToList();
    }
}