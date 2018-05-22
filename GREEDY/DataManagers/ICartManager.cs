using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GREEDY.Data;

namespace GREEDY.DataManagers
{
    public interface ICartManager
    {
        CartDataModel GetCart(string username);

        CartDataModel UpdateCart(CartDataModel newCart, string username);
    }
}
