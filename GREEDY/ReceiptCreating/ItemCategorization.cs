using GREEDY.Models;
using MoreLinq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace GREEDY.ReceiptCreatings
{
    public class ItemCategorization : IItemCategorization
    {
        private static NaiveBayesianClassifier _classifier;
        private static List<ItemClassificationModels> _trainingData;

        public ItemCategorization()
        {
            ReadCategories();
        }

        public List<Item> CategorizeItems(List<Item> itemList)
        {
            _classifier = new NaiveBayesianClassifier(_trainingData);

            var NewData = itemList.Select(x => new ItemClassificationModels { Category = x.Category, Text = x.Name, Prob = 0 }).ToList();
            NewData = _classifier.GetAllItemsWithCategories(NewData);

            foreach (Item item in itemList)
            {
                foreach (ItemClassificationModels itemInfo in NewData)
                {
                    if (item.Name == itemInfo.Text && item.Category == String.Empty)
                    {
                        item.Category = itemInfo.Category;
                    }
                }
            }
            //TODO: add data to training is working perfectry, but I commented this part, because during
            //testing we chare a lot of trash data. So, we need to think about diferent way to update training data

            //AddNewDataToTrainingData(NewData);
            return itemList;
        }

        private void UpdateClassifier() => _classifier = new NaiveBayesianClassifier(_trainingData);

        // reads info from file
        public static void ReadCategories()
        {
            if (!File.Exists(Environments.AppConfig.CategoriesDataPath))
            {
                try
                {
                    File.Create(Environments.AppConfig.CategoriesDataPath);
                }
                catch
                {
                    //TODO: need to think about possible exception if training data is missing
                }
            }
            else
            {
                _trainingData = JsonConvert.DeserializeObject<List<ItemClassificationModels>>
                    (File.ReadAllText(Environments.AppConfig.CategoriesDataPath));
            }
            if (_trainingData == null)
            {
                _trainingData = new List<ItemClassificationModels>();
            }
        }

        private static async void AddNewDataToTrainingData(List<ItemClassificationModels> NewData)
        {
            using (StreamWriter writer = File.CreateText(Environments.AppConfig.CategoriesDataPath))
            {
                _trainingData = _trainingData.Union(NewData).ToList();
                _trainingData = _trainingData.DistinctBy(o => o.Text).ToList();
                await writer.WriteLineAsync(JsonConvert.SerializeObject(_trainingData));
            }
        }
        public void AddCategory(string itemName, string category)
        {
            if (itemName != null && category != null)
            {
                File.WriteAllText(Environments.AppConfig.CategoriesDataPath, JsonConvert.SerializeObject(
                new ItemClassificationModels { Text = itemName, Category = category, Prob = 0 }));
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}