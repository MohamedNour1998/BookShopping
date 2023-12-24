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
    public class CartRep :ICartRep
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CartRep(ApplicationDbContext context, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }
        public ShoppingCart GetCart(string userId)
        {
            var cart = context.ShoppingCarts.FirstOrDefault(e => e.UserId == userId);
            return cart;
        }
        public string GetUserID()
        {
            var PrincipalUser = httpContextAccessor.HttpContext.User;
            string user = userManager.GetUserId(PrincipalUser);
            return user;
        }
        public async Task <int> AddItem(int bookId,int Qty)
        {
            string userId = GetUserID();
            using var transaction = context.Database.BeginTransaction();
            try
            {
               
                var cart = GetCart(userId);
                if (cart is null)
                {
                    cart = new ShoppingCart
                    {
                        UserId = userId
                    };
                    context.ShoppingCarts.Add(cart);
                }
                context.SaveChanges();
                var cartItem = await context.CartDetails.FirstOrDefaultAsync(e=>e.ShoppingCartId==cart.Id&&e.BookId==bookId);
                if (cartItem != null)
                {
                    cartItem.Quantity += Qty;
                }
                else
                {
                    cartItem = new CartDetail
                    {
                        BookId=bookId,
                        ShoppingCartId=cart.Id,
                        Quantity=Qty
                    };
                    context.CartDetails.Add(cartItem);
                }
                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }

        public async Task<int> RemoveItemAsync(int bookId)
        {
            string userId = GetUserID();
            //using var transaction = context.Database.BeginTransaction();
            try
            {
                
                var cart = GetCart(userId);
                if (cart is null)
                {
                    throw new Exception("cart is null");
                }
                var cartItem = context.CartDetails.FirstOrDefault(e => e.ShoppingCartId == cart.Id && e.BookId == bookId);

                if (cartItem==null)
                {
                    throw new Exception("cartItem is null");
                }
                else if (cartItem.Quantity == 1)
                {
                    context.CartDetails.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity = cartItem.Quantity - 1;
                }
                context.SaveChanges();
                //transaction.Commit();
               

            }
            catch (Exception ex)
            {
                
            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }

        public async Task<ShoppingCart> GetUserCart()
        {
            var userId = GetUserID();
            if (userId==null)
            {
                throw new Exception("invalid User");
            }
            var shoppingCart =await context.ShoppingCarts.Include(e => e.cartDetails).ThenInclude(e => e.Book)
                .ThenInclude(e => e.Genre).Where(e => e.UserId == userId).FirstOrDefaultAsync();
            return shoppingCart;
        }

        public async Task<int> GetCartItemCount(string userId="")
        {
            if (!string.IsNullOrEmpty(userId))
            {
                userId = GetUserID();
            }
            var data = await (from cart in context.ShoppingCarts
                              join cartDetail in context.CartDetails
                              on cart.Id equals cartDetail.ShoppingCartId
                              select new { cartDetail.Id}
                             ).ToListAsync();
            return data.Count();

        }

        public async Task<bool> DoCheckOut()
        {
            var transaction = context.Database.BeginTransaction();
            try
            {
                var userId = GetUserID();
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User is not Login");
                var cart = GetCart(userId);
                if(cart==null)
                    throw new Exception("not cart Found");
                var cartDetails =await context.CartDetails.Where(e => e.ShoppingCart.Id == cart.Id).ToListAsync();
                if(cartDetails.Count==0)
                    throw new Exception("cart Found is empty");
                var order = new Order
                {
                    UserId=userId,
                    CreateDate=DateTime.UtcNow,
                    OrderStatusId=1
                };
                context.Orders.Add(order);
                context.SaveChanges();
                foreach (var item in cartDetails)
                {
                    var orderDetails = new OrderDetails
                    {
                        BookId=item.BookId,
                        OrderId=order.Id,
                        Quantity=item.Quantity,
                        UnitPrice=item.UnitPrice
                    };
                    context.OrderDetails.Add(orderDetails);
                }
                context.SaveChanges();
                context.CartDetails.RemoveRange(cartDetails);
                context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }



    }
}
