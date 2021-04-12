using System.Collections.Generic;
using System.Threading.Tasks;
using EShop.Data;
using EShop.Data.Entities;
using EShop.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EShop.Pages.Management.Products
{
    [Authorize(Roles = "Administration")]
    public class Index : PageModel
    {
        private readonly ShopContext _shopContext;

        public Index(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        
        public PaginatedList<ProductEntity> Products { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int pageSize = 6, int page = 1)
        {
            Products = await PaginatedList<ProductEntity>.CreateAsync(
                _shopContext.Products.Include(x=>x.Images).AsNoTracking(), page, pageSize);
            return Page();
        }
    }
}