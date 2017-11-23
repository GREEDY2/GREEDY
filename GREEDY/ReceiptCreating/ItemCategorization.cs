using GREEDY.Models;
using MoreLinq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GREEDY.ReceiptCreatings
{
    public class ItemCategorization : IItemCategorization
    {
        public static List<ItemInfo> _info = new List<ItemInfo>();
        private static NaiveBayesianClassifier _NBClassifier;

        public ItemCategorization()
        {
            ReadCategories();
            _NBClassifier = new NaiveBayesianClassifier(_info);
        }
        
        public List<ItemInfo> CategorizeAllItems(List<ItemInfo> NewData)
        {
            (_info, NewData) = _NBClassifier.GetAllItemsWithCategories(NewData);
            _info = _info.DistinctBy(o => o.Text).ToList();
            WriteCategories();
            return NewData;
        }
       
        // updates classifier trained data
        private void UpdateClassifier()
        {
            _NBClassifier = new NaiveBayesianClassifier(_info);
        }

        // reads info from file
        private static void ReadCategories()
        {
            if (!File.Exists(Environments.AppConfig.CategoriesDataPath))
            {
                //File.Create(Environments.AppConfig.CategoriesDataPath);
                // TODO:Fix problem with file creation, 
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
        public void WriteCategories()
        {
            File.WriteAllText(Environments.AppConfig.CategoriesDataPath, JsonConvert.SerializeObject(_info));
        }
    }
}