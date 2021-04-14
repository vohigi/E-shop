using System;
using System.Threading.Tasks;
using EShop.Data;
using EShop.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EShop.Pages.Order
{
    [Authorize()]
    public class Details : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ShopContext _shopContext;

        public Details(ShopContext shopContext, UserManager<ApplicationUser> userManager)
        {
            _shopContext = shopContext;
            _userManager = userManager;
        }
        public ApplicationUser CurrentUser { get; set; }
        public OrderEntity Order { get; set; }
        
        public bool JustCreated { get; set; }
        public bool JustPaid { get; set; }
        
        public async Task<IActionResult> OnGet(Guid? id, bool justCreated = false, bool justPaid = false)
        {
            if (!id.HasValue)
            {
                return RedirectToPage("/Index");
            }

            JustCreated = justCreated;
            JustPaid = justPaid;
            var user = await _userManager.GetUserAsync(Request.HttpContext.User);
            CurrentUser = user;
            var order = await _shopContext.Orders.Include(x => x.ShoppingCart).ThenInclude(x => x.CartItems)
                .ThenInclude(x => x.Item).ThenInclude(x => x.Images).Include(x => x.PaymentType).Include(x => x.ShippingType)
                .FirstOrDefaultAsync(x => x.Id == id);
            Order = order;
            return Page();
        }
    }
}