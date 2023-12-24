using BookShopping.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShopping
{
    public interface IUserOrderRep
    {
        Task<IEnumerable<Order>> UserOrders();
    }
}