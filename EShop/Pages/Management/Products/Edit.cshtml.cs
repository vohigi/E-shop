using System;
using System.Threading.Tasks;
using EShop.Data;
using EShop.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.Pages.Management.Products
{
    [Authorize(Roles = "Administration")]
    public class Edit : PageModel
    {
        private readonly ShopContext _shopContext;

        public Edit(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        
        [BindProperty]
        public ProductEntity Product { get; set; }
        
        public async Task<IActionResult> OnGet(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _shopContext.Products.FindAsync(id);

            if (Product == null)
            {
                return NotFound();
            }
            
            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var productToUpdate = await _shopContext.Products.FindAsync(id);

            if (await TryUpdateModelAsync<ProductEntity>(
                productToUpdate,
                "product",
                s => s.DisplayName, 
                s => s.Description, 
                s => s.Stock,
                s => s.Price,
                s => s.Manufacturer,
                s => s.OperatingSystem, 
                s => s.Rom,
                s => s.Ram,
                s => s.NumberOfSimCards,
                s => s.ProcessorName,
                s => s.Clock,
                s => s.NumberOfCores,
                s => s.HasSdCardSlot,
                s => s.BatteryCapacity,
                s => s.DisplayDiagonal,
                s => s.DisplayHeight,
                s => s.DisplayWidth,
                s => s.DisplayType,
                s => s.RearCamera,
                s => s.FrontCamera,
                s => s.HasNFC,
                s => s.Weight,
                s => s.Width,
                s => s.Height,
                s => s.Thickness
                ))
            {
                await _shopContext.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }        
    }
}