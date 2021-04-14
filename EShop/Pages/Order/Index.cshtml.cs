using System;
using System.Collections.Generic;
using System.Linq;
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
    public class Index : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ShopContext _shopContext;

        public Index(ShopContext shopContext, UserManager<ApplicationUser> userManager)
        {
            _shopContext = shopContext;
            _userManager = userManager;
        }
        public ApplicationUser CurrentUser { get; set; }
        public List<OrderEntity> Orders { get; set; }
        
        public async Task<IActionResult> OnGet()
        {
            var userId = _userManager.GetUserId(Request.HttpContext.User);
            var orders = await _shopContext.Orders.Include(x => x.PaymentType).Include(x => x.ShippingType).Include(x => x.ShoppingCart)
                .Where(x => x.CustomerId == Guid.Parse(userId)).ToListAsync();
            Orders = orders;
            return Page();
        }
    }
}