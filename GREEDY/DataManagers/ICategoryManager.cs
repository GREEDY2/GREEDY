using System.Collections.Generic;

namespace GREEDY.DataManagers
{
    public interface ICategoryManager
    {
        List<string> GetAllDistinctCategories();
    }
}