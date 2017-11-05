using System.Collections.Generic;
using GREEDY.Models;
using GREEDY.Data;
using System.Linq;

namespace GREEDY.DataManagers
{
    public class CategoryManager : ICategoryManager
    {
        public List<string> GetAllDistinctCategories()
        {
            using (DataBaseModel context = new DataBaseModel())
            {
                var distinctCategories = context.Set<CategoryDataModel>()
                    .Select(x => x.Category).Distinct();
                return distinctCategories.ToList();
            }
        }
    }
}
