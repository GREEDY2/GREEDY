using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GREEDY.DataManagers;
using GREEDY.Models;

namespace GREEDY.Services
{
    public class CartService : ICartService
    {
        private readonly ICartManager _cartManager;

        public CartService(ICartManager cartManager)
        {
            _cartManager = cartManager;
        }

        public Cart GetCart(string username)
        {
            return new Cart(_cartManager.GetCart(username));
        }

        public Cart UpdateCart(Cart newCart, string username)
        {
            return new Cart(_cartManager.UpdateCart(new Data.CartDataModel(newCart), username));
        }
    }
}
