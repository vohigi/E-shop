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
        
        public async Task<IActionResult> OnGet()
        {
            var userId = _userManager.GetUserId(Request.HttpContext.User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login");
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
    }
}