using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace GREEDY.DataManagers
{
    public class ItemCategorization : IItemCategorization
    {
        private static Dictionary<string, string> _categoriesDictionary;
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
            foreach (KeyValuePair<string, string> category in _categoriesDictionary)
            {
                if (itemName.ToLower().Contains(category.Key))
                {
                    itemCategory = category.Value;
                }
            }
            return itemCategory;
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
                _categoriesDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>
                    (File.ReadAllText(Environments.AppConfig.CategoriesDataPath));
            }
            if (_categoriesDictionary == null)
            {
                _categoriesDictionary = new Dictionary<string, string>();
            }
        }

        public void AddChangeCategories(string itemName, string category)
        {
            if (!_categoriesDictionary.ContainsKey(itemName))
            {
                _categoriesDictionary.Add(itemName, category);
                File.WriteAllText(Environments.AppConfig.CategoriesDataPath, JsonConvert.SerializeObject(_categoriesDictionary));
            }
            else if (_categoriesDictionary[itemName] != category)
            {
                _categoriesDictionary[itemName] = category;
                File.WriteAllText(Environments.AppConfig.CategoriesDataPath, JsonConvert.SerializeObject(_categoriesDictionary));
            }
        }
    }
}
