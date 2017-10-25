using GREEDY.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GREEDY.DataManagers
{
    public class ItemCategorization : IItemCategorization
    {
        //private static Dictionary<string, string> _categoriesDictionary;
        private static List<ItemInfo> _info = new List<ItemInfo>();
        static ItemCategorization()
        {
            UpdateCategories();
        }
        // TODO: write a more flexible item categorization
        // decimal price is not needed for now, 
        // but maybe available implementation for the future to help categorization
        public string CategorizeSingleItem(string itemName, decimal price = 0)
        {
            string itemCategory = string.Empty;
            NaiveBayesianClassifier c = new NaiveBayesianClassifier(_info);
            return c.GetTopCategory(itemName.ToLower());

            /* TODO: GetXTopCategories beveik neveikia, update catergorylist
            
            foreach (KeyValuePair<string, string> category in _categoriesDictionary)
            {
                if (itemName.ToLower().Contains(category.Key))
                {
                    itemCategory = category.Value;
                }
            }*/
            //return itemCategory;
        }

        private static void UpdateCategories()
        {
            if (!File.Exists(Environments.AppConfig.CategoriesDataPath))
            {
                //File.Create(Environments.AppConfig.CategoriesDataPath);
                // TO DO:Fix problem with file creation, 
                // line 56 throws an exeption when changing category, if file was created this way
            }
            else
            {
                _info = JsonConvert.DeserializeObject<List<ItemInfo>>
                    (File.ReadAllText(Environments.AppConfig.CategoriesDataPath));
            }
            if (_info == null)
            {
                _info = new List<ItemInfo>();
            }
        }

        public void AddChangeCategories(string itemName, string category)
        {
            ItemInfo i = new ItemInfo(category, itemName);
            _info.Add(i);
            File.WriteAllText(Environments.AppConfig.CategoriesDataPath, JsonConvert.SerializeObject(_info));

            /*if (!_categoriesDictionary.ContainsKey(itemName))
            {
                _categoriesDictionary.Add(itemName, category);
                File.WriteAllText(Environments.AppConfig.CategoriesDataPath, JsonConvert.SerializeObject(_categoriesDictionary));
            }
            else if (_categoriesDictionary[itemName] != category)
            {
                _categoriesDictionary[itemName] = category;
                File.WriteAllText(Environments.AppConfig.CategoriesDataPath, JsonConvert.SerializeObject(_categoriesDictionary));
            }*/
        }
    }
}
