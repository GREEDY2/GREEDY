using GREEDY.Models;
using MoreLinq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GREEDY.DataManagers;

namespace GREEDY.ReceiptCreatings
{
    public class ItemCategorization : IItemCategorization
    {
        //private static Dictionary<string, string> _categoriesDictionary;
        private static List<ItemInfo> _info = new List<ItemInfo>();
        private static NaiveBayesianClassifier c = new NaiveBayesianClassifier(_info);
        static ItemCategorization()
        {
            UpdateCategories();
            c = new NaiveBayesianClassifier(_info);
        }

        /*public string CategorizeSingleItem(string itemName, decimal price = 0)
        {
            string itemCategory = string.Empty;
            //itemCategory = c.GetTopCategory(itemName.ToLower()); // this works too. using the other one for testing
            itemCategory = c.GetAllCategoriesSorted(itemName.ToLower()).First();
            AddItemToInfo(itemName, itemCategory);
            UpdateClassifier();
            AddChangeCategories();
            return itemCategory;
        }*/
        public List<ItemInfo> CategorizeAllItems(List<ItemInfo> NewData)
        {
            NewData = c.GetAllItemsWithCategories(NewData);
            _info = c.Info;
            _info = _info.DistinctBy(o => o.Text).ToList();
            AddChangeCategories();
            return NewData;
        }
       

        // updates classifier trained data
        private void UpdateClassifier()
        {
            c = new NaiveBayesianClassifier(_info);
        }

        // reads info from file
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
        
        // writes info into file
        public void AddChangeCategories()
        {
            File.WriteAllText(Environments.AppConfig.CategoriesDataPath, JsonConvert.SerializeObject(_info));
        }
    }
}