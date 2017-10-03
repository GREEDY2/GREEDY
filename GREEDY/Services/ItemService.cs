using System.Collections.Generic;
using GREEDY.DataManagers;
using GREEDY.Models;
using GREEDY.OCRs;
using System.Drawing;
using System;

namespace GREEDY.Services
{
    public class ItemService : IItemService
    {
        private readonly IDataConverter _dataConverter;
        private readonly IDataManager _dataManager;
        private readonly IItemCategorization _itemCategorization;

        public ItemService(
            IDataConverter dataConverter, IDataManager dataManager, IItemCategorization itemCategorization)
        {
            _dataConverter = dataConverter;
            _dataManager = dataManager;
            _itemCategorization = itemCategorization;
        }

        public void AddChangeCategory(string itemName, string category)
        {
            _itemCategorization.AddChangeCategories(itemName.ToLower(), category.ToLower());
        }
    }
}