using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GREEDY.Data;

namespace GREEDY.DataManagers
{
    public class CategoryManager : ICategoryManager, IDisposable
    {
        private readonly DbContext _context;

        public CategoryManager(DbContext context)
        {
            _context = context;
        }

        public List<string> GetAllDistinctCategories () => _context.Set<CategoryDataModel> ().Select (x => x.CategoryName).ToList ();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}