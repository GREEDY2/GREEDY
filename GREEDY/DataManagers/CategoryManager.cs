using System.Collections.Generic;
using GREEDY.Data;
using System.Linq;
using System.Data.Entity;
using System;

namespace GREEDY.DataManagers
{
    public class CategoryManager : ICategoryManager,IDisposable
    {
        private DbContext context;

        public CategoryManager(DbContext context)
        {
            this.context = context;
        }
        public void Dispose()
        {
            context.Dispose();
        }

        public List<string> GetAllDistinctCategories()
        {
            {
                var distinctCategories = context.Set<CategoryDataModel>()
                    .Select(x => x.Category).Distinct();
                return distinctCategories.ToList();
            }
        }
       
    }
}