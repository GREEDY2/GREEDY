using GREEDY.Models;

namespace GREEDY.Services
{
    public interface ICartService
    {
        Cart GetCart(string username);

        Cart UpdateCart(Cart newCart, string Username);
    }
}
