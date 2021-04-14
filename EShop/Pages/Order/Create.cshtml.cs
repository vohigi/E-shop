using System;
using System.Collections.Generic;
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
    public class Create : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ShopContext _shopContext;

        public Create(ShopContext shopContext, UserManager<ApplicationUser> userManager)
        {
            _shopContext = shopContext;
            _userManager = userManager;
        }
        public ApplicationUser CurrentUser { get; set; }
        public ShoppingCartEntity ShoppingCart { get; set; }
        public List<ShippingTypeEntity> ShippingTypes { get; set; }
        public List<PaymentTypeEntity> PaymentTypes { get; set; }
        
        [BindProperty]
        public Guid? ShoppingCartId { get; set; }
        [BindProperty]
        public Guid? PaymentTypeId { get; set; }
        [BindProperty]
        public Guid? ShippingTypeId { get; set; }
        
        public async Task<IActionResult> OnGet(Guid? id)
        {
            if (!id.HasValue)
            {
                return RedirectToPage("/Index");
            }
            var user = await _userManager.GetUserAsync(Request.HttpContext.User);
            CurrentUser = user;
            var shoppingCart = await _shopContext.ShoppingCarts.Include(x => x.CartItems).ThenInclude(x => x.Item).ThenInclude(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == id);
            ShoppingCart = shoppingCart;
            ShippingTypes = await _shopContext.ShippingTypes.ToListAsync();
            PaymentTypes = await _shopContext.PaymentTypes.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var userId = _userManager.GetUserId(Request.HttpContext.User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login",new { area = "Identity" });
            }
            var shoppingCart = await _shopContext.ShoppingCarts.Include(x => x.CartItems).ThenInclude(x => x.Item).ThenInclude(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == ShoppingCartId);
            var payment = await _shopContext.PaymentTypes.FirstOrDefaultAsync(x => x.Id == PaymentTypeId);
            var shipping = await _shopContext.ShippingTypes.FirstOrDefaultAsync(x => x.Id == ShippingTypeId);
            shoppingCart.Order = new OrderEntity()
            {
                CustomerId = Guid.Parse(userId),
                PaymentType = payment,
                ShippingType = shipping,
                TotalPrice = shoppingCart.TotalPrice
            };
            _shopContext.ShoppingCarts.Update(shoppingCart);
            await _shopContext.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}