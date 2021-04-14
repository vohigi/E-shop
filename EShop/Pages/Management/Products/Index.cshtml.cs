using System.Collections.Generic;
using System.Linq;
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
        public string CurrentFilter { get; set; }

        public Index(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        
        public PaginatedList<ProductEntity> Products { get; set; }
        
        public async Task<IActionResult> OnGetAsync(string searchString, int pageSize = 6, int page = 1)
        {
            CurrentFilter = searchString;
            Products = PaginatedList<ProductEntity>.CreateAsync(
                await _shopContext.Products.Include(x => x.Images).AsNoTracking().Where(s =>
                    string.IsNullOrEmpty(searchString) ||
                    (s.DisplayName.ToUpper().Contains(searchString.ToUpper()) ||
                     s.Description.ToUpper().Contains(searchString.ToUpper()))).ToListAsync(), page, pageSize);
            return Page();
        }
    }
}