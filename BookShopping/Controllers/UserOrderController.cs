using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShopping.Controllers
{
    [Authorize]
    public class UserOrderController : Controller
    {
        private readonly IUserOrderRep userOrderRepo;

        public UserOrderController(IUserOrderRep userOrderRepo)
        {
            this.userOrderRepo = userOrderRepo;
        }
        public async Task<IActionResult> UserOrders()
        {
            var orders = await userOrderRepo.UserOrders();
            return View(orders);
        }
    }
}
