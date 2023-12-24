using BookShopping.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShopping
{
    public interface ICartRep
    {
        Task<int> AddItem(int bookId, int Qty);
        Task<int> RemoveItemAsync(int bookId);
        Task<int> GetCartItemCount(string userId = "");
        Task<ShoppingCart> GetUserCart();
        ShoppingCart GetCart(string userId);
        Task<bool> DoCheckOut();
    }
}