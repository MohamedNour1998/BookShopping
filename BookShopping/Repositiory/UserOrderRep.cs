using BookShopping.Data;
using BookShopping.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShopping.Repositiory
{
    public class UserOrderRep:IUserOrderRep
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserOrderRep(ApplicationDbContext context, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<Order>> UserOrders()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User is not logged-in");
            var orders = await context.Orders
                            .Include(x => x.OrderStatus)
                            .Include(x => x.OrderDetails)
                            .ThenInclude(x => x.Book)
                            .ThenInclude(x => x.Genre)
                            .Where(a => a.UserId == userId)
                            .ToListAsync();
            return orders;
        }

        private string GetUserId()
        {
            var principal = httpContextAccessor.HttpContext.User;
            string userId = userManager.GetUserId(principal);
            return userId;
        }


    }
}
