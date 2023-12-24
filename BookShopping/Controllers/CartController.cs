using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShopping.Controllers
{
    [Authorize]
    [ValidateAntiForgeryToken]
    public class CartController : Controller
    {
        private readonly ICartRep cartRep;

        public CartController(ICartRep cartRep)
        {
            this.cartRep = cartRep;
        }
        public async Task <IActionResult> AddItem(int bookId,int qty=1,int reqirect=0)
        {
            var cartCount =await cartRep.AddItem(bookId, qty);
            if (reqirect==0)
            {
                return Ok(cartCount);
            }
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> RemoveItem(int bookId, int qty = 1)
        {
            var cartCount =await cartRep.RemoveItemAsync(bookId);
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> GetUserCart()
        {
            var cart = await cartRep.GetUserCart();
            return View("GetUserCart", cart);
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            var cartItem = await cartRep.GetCartItemCount();
            return Ok(cartItem);
        }
        public async Task<IActionResult> ChexkOut()
        {
            bool Ischecked =await cartRep.DoCheckOut();
            if (!Ischecked)
                throw new Exception("Error in server side");
            return RedirectToAction("Index","Home");
        }

    }
}
