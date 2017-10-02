using GREEDY.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GREEDY.DataManagers
{
    class ItemCategorization
    {
        private static Dictionary<string, string> CategoriesDictionary = ReadCategoriesDictionary();
        // TO DO: write a more flexible item categorization
        // decimal price is not needed for now, 
        // but maybe available implementation for the future to help categorization
        // File.WriteAllText(Environments.AppConfig.CategoriesDataPath, JsonConvert.SerializeObject(CategoriesDictionary));
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

        private static Dictionary<string, string> ReadCategoriesDictionary()
        {
            Dictionary<string, string> CategoriesDictionary;
            try
            {
                CategoriesDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>
                    (File.ReadAllText(Environments.AppConfig.CategoriesDataPath));
            }
            catch (FileNotFoundException ex)
            {
                // TO DO: Create categories.txt in Resources folder
                CategoriesDictionary = new Dictionary<string, string>();
            }
            return CategoriesDictionary;
        }
    }
}
