using System;
using System.Linq;
using System.Threading.Tasks;
using EShop.Data;
using EShop.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EShop.Pages.Customer
{
    public class PaymentSystem : PageModel
    {
        private readonly ShopContext _shopContext;
        public PaymentSystem(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        
        public OrderEntity Order { get; set; }
        
        public async Task<IActionResult> OnGet(Guid id)
        {
            Order = await _shopContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
            return Page();
        }
    }
}