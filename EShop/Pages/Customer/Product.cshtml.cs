using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Data;
using EShop.Data.Entities;
using EShop.Data.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EShop.Pages.Customer
{
    public class Product : PageModel
    {
        private readonly ShopContext _shopContext;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public Product(ShopContext shopContext, UserManager<ApplicationUser> userManager)
        {
            _shopContext = shopContext;
            _userManager = userManager;
        }

        public Dictionary<string, string> SpecificationsList { get; set; }
        
        public string Specifications { get; set; }
        public ProductEntity ProductEntity { get; set; }
        public string Availability { get; set; }
        public async Task<IActionResult> OnGet(string id)
        {
            ProductEntity = await _shopContext.Products
                .AsNoTracking()
                .Include(x => x.Images)
                .Where(x => x.Id == Guid.Parse(id)).FirstOrDefaultAsync();
            SpecificationsList = TextHelper.PrepareSpecifications(ProductEntity);
            return Page();
        }
        public async Task<IActionResult> OnPostAddToCart(Guid? id)
        {
            var userId = _userManager.GetUserId(Request.HttpContext.User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login");
            }

            var product = await _shopContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            var existingShoppingCart =
                await _shopContext.ShoppingCarts.Include(x => x.CartItems).ThenInclude(x => x.Item)
                    .ThenInclude(x => x.Images).FirstOrDefaultAsync(
                        x =>
                            x.CustomerId == userId && !x.OrderId.HasValue);
            var existingCartItem = existingShoppingCart.CartItems?.FirstOrDefault(x => x.ItemId == id);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
                _shopContext.CartItems.Update(existingCartItem);
                await _shopContext.SaveChangesAsync();
                return RedirectToPage("./Product", new {id});
            }

            if (existingShoppingCart.CartItems == null)
            {
                existingShoppingCart.CartItems = new List<CartItemEntity>();
            }
            existingShoppingCart.CartItems.Add(new CartItemEntity()
            {
                Item = product,
                Quantity = 1
            });
            _shopContext.ShoppingCarts.Update(existingShoppingCart);
            await _shopContext.SaveChangesAsync();
            return RedirectToPage("./Product", new {id});
        }
    }
}