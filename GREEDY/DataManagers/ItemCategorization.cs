using GREEDY.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GREEDY.DataManagers
{
    class ItemCategorization : IItemCategorization
    {
        private static Dictionary<string, string> CategoriesDictionary;
        static ItemCategorization()
        {
            UpdateCategories();
        }
        // TO DO: write a more flexible item categorization
        // decimal price is not needed for now, 
        // but maybe available implementation for the future to help categorization
        public string CategorizeSingleItem(string itemName, decimal price = 0)
        {
            string itemCategory = string.Empty;
            foreach (KeyValuePair<string, string> category in CategoriesDictionary)
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
                CategoriesDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>
                    (File.ReadAllText(Environments.AppConfig.CategoriesDataPath));
            }
            if (CategoriesDictionary == null)
            {
                CategoriesDictionary = new Dictionary<string, string>();
            }
        }

        public void AddChangeCategories(string itemName, string category)
        {
            if (!CategoriesDictionary.ContainsKey(itemName))
            {
                CategoriesDictionary.Add(itemName, category);
                File.WriteAllText(Environments.AppConfig.CategoriesDataPath, JsonConvert.SerializeObject(CategoriesDictionary));
            }
            else if (CategoriesDictionary[itemName] != category)
            {
                CategoriesDictionary[itemName] = category;
                File.WriteAllText(Environments.AppConfig.CategoriesDataPath, JsonConvert.SerializeObject(CategoriesDictionary));
            }
        }
    }
}
