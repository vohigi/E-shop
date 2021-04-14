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
    public class Product : PageModel
    {
        private readonly ShopContext _shopContext;

        public Product(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public string Specifications { get; set; }
        public ProductEntity ProductEntity { get; set; }
        public async Task<IActionResult> OnGet(string id)
        {
            ProductEntity = await _shopContext.Products
                .AsNoTracking()
                .Include(x => x.Images)
                .Where(x => x.Id == Guid.Parse(id)).FirstOrDefaultAsync();
            return Page();
        }

        private string SetSpecifications()
        {
            if (ProductEntity == null) return string.Empty;
            
            return "";
        }
    }
}