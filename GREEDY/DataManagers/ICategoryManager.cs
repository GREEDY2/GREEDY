using System.Collections.Generic;
using System.Drawing;
using GREEDY.Models;

namespace GREEDY.DataManagers
{
    public interface ICategoryManager
    {
        List<string> GetAllDistinctCategories();
    }
}