using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Data;
using EShop.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EShop.Pages
{
    
    public class Cart : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ShopContext _shopContext;

        public Cart(ShopContext shopContext, UserManager<ApplicationUser> userManager)
        {
            _shopContext = shopContext;
            _userManager = userManager;
        }
        
        public ShoppingCartEntity ShoppingCart { get; set; }
        [BindProperty]
        public int NewQuantity { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var userId = _userManager.GetUserId(Request.HttpContext.User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login",new { area = "Identity" });
            }

            var existingShoppingCart =
                await _shopContext.ShoppingCarts.Include(x => x.CartItems).ThenInclude(x => x.Item)
                    .ThenInclude(x => x.Images).FirstOrDefaultAsync(
                        x =>
                            x.CustomerId == userId && !x.OrderId.HasValue);
            if (existingShoppingCart == null)
            {
            }

            ShoppingCart = existingShoppingCart;
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveFromCart(Guid? id)
        {
            var userId = _userManager.GetUserId(Request.HttpContext.User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login",new { area = "Identity" });
            }
            var existingShoppingCart =
                await _shopContext.ShoppingCarts.Include(x => x.CartItems).ThenInclude(x => x.Item)
                    .ThenInclude(x => x.Images).FirstOrDefaultAsync(
                        x =>
                            x.CustomerId == userId && !x.OrderId.HasValue);
            var existingCartItem = existingShoppingCart.CartItems?.FirstOrDefault(x => x.Id == id);
            if (existingCartItem == null) 
                return RedirectToPage("./Cart");
            _shopContext.CartItems.Remove(existingCartItem);
            await _shopContext.SaveChangesAsync();
            return RedirectToPage("./Cart");
        }
        public async Task<IActionResult> OnPostChangeCount(Guid? id)
        {
            var userId = _userManager.GetUserId(Request.HttpContext.User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login",new { area = "Identity" });
            }
            var existingShoppingCart =
                await _shopContext.ShoppingCarts.Include(x => x.CartItems).ThenInclude(x => x.Item)
                    .ThenInclude(x => x.Images).FirstOrDefaultAsync(
                        x =>
                            x.CustomerId == userId && !x.OrderId.HasValue);
            var existingCartItem = existingShoppingCart.CartItems?.FirstOrDefault(x => x.Id == id);
            if (existingCartItem == null) 
                return RedirectToPage("./Cart");
            existingCartItem.Quantity = NewQuantity;
            _shopContext.CartItems.Update(existingCartItem);
            await _shopContext.SaveChangesAsync();
            return RedirectToPage("./Cart");
        }
    }
}