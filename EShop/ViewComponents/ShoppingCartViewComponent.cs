using System;
using System.Linq;
using System.Threading.Tasks;
using EShop.Data;
using EShop.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EShop.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ShopContext _shopContext;

        public ShoppingCartViewComponent(ShopContext shopContext, UserManager<ApplicationUser> userManager)
        {
            _shopContext = shopContext;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _userManager.GetUserId(Request.HttpContext.User);
            if (string.IsNullOrEmpty(userId))
            {
                return View(0);
            }

            var existingShoppingCart =
                await _shopContext.ShoppingCarts.Include(x=>x.CartItems).FirstOrDefaultAsync(x =>
                    x.CustomerId == userId && !x.OrderId.HasValue);
            if (existingShoppingCart == null)
            {
                _shopContext.ShoppingCarts.Add(new ShoppingCartEntity
                {
                    CreatedAt = DateTime.Now,
                    CustomerId = userId,
                    LastUpdatedAt = DateTime.Now
                });
                await _shopContext.SaveChangesAsync();
                return View(0);
            }

            return View(existingShoppingCart.CartItems?.Sum(x=>x.Quantity) ?? 0);
        }
    }
}