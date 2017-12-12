using GREEDY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GREEDY.ReceiptCreatings
{
    //extract features and prepare for other classification (MODEL)
    public class ClassInfo
    {
        public ClassInfo(string name, List<String> trainDocs)
        {
            Name = name;
            var features = trainDocs.SelectMany(x => x.ExtractFeatures());
            WordsCount = features.Count();
            WordCount = features.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            NumberOfDocs = trainDocs.Count;
        }

        public string Name { get; set; }
        public int WordsCount { get; set; }
        public Dictionary<string, int> WordCount { get; set; }
        public int NumberOfDocs { get; set; }
        public int NumberOfOccurencesInTrainDocs(String word)
        {
            if (WordCount.Keys.Contains(word))
            {
                return WordCount[word];
            }
            return 0;
        }
    }

    public class NaiveBayesianClassifier
    {
        List<ClassInfo> _classes;
        int _countOfDocs;
        int _uniqWordsCount;

        public NaiveBayesianClassifier(List<ItemClassificationModels> _trainingData)
        {
            ClassifierTraining(_trainingData);
        }

        public List<ItemClassificationModels> GetAllItemsWithCategories(List<ItemClassificationModels> NewData)
        {
            double maxProb = 0;
            string maxCat = "";
            double prob = 0;

            foreach (ItemClassificationModels item in NewData)
            {
                if (item.Category == String.Empty)
                {
                    foreach (string category in _classes.Select(x => x.Name).Distinct())
                    {
                        prob = IsInClassProbability(category, item.Text);

                        if (prob > maxProb)
                        {
                            maxProb = prob;
                            maxCat = category;
                        }
                    }
                    item.Category = maxCat;
                    item.Prob = maxProb;
                    maxProb = 0;
                    maxCat = String.Empty;
                }
            }
            return NewData;
        }

        public double IsInClassProbability(string category, string text)
        {
            var words = text.ExtractFeatures();
            var classResults = _classes
                .Select(x => new
                {
                    Result = Math.Pow(Math.E, Calc(x.NumberOfDocs, _countOfDocs, words, x.WordsCount, x, _uniqWordsCount)),
                    ClassName = x.Name
                });
            return classResults.Single(x => x.ClassName == category).Result / classResults.Sum(x => x.Result);
        }

        private static double Calc(double dc, double d, List<String> q, double lc, ClassInfo classInfo, double v)
        {
            return Math.Log(dc / d) + q.Sum(x => Math.Log((classInfo.NumberOfOccurencesInTrainDocs(x) + 1) / (v + lc)));
        }

        private void ClassifierTraining(List<ItemClassificationModels> train)
        {
            _classes = train.GroupBy(x => x.Category).Select(g => new ClassInfo(g.Key, g.Select(x => x.Text).ToList())).ToList();
            _countOfDocs = train.Count;
            _uniqWordsCount = train.SelectMany(x => x.Text.Split(' ')).GroupBy(x => x).Count();
        }
    }

    public static class Helpers
    {
        public static List<String> ExtractFeatures(this String text)
        {
            return Regex.Matches(text, "\\p{L}{4,}").Cast<Match>().Select(match => match.Value).ToList();
        }
    }
}