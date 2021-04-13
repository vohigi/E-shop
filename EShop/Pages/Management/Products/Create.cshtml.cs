using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using EShop.Data;
using EShop.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.Pages.Management.Products
{
    [Authorize(Roles = "Administration")]
    public class Create : PageModel
    {
        private readonly ShopContext _shopContext;

        public Create(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        
        [BindProperty]
        public ProductEntity Product { get; set; }
        
        [BindProperty]
        public IList<IFormFile> ImageFiles { get; set; }

        public async Task<IActionResult> OnGet()
        {
            CultureInfo uiCultureInfo = Thread.CurrentThread.CurrentUICulture;
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Product.Images = new List<ImageEntity>();
            foreach (var file in ImageFiles)
            {
                if (file.Length > 0)
                {
                    await using var ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();
                    Product.Images.Add(new ImageEntity
                    {
                        ImageData = fileBytes
                    });
                    // act on the Base64 data
                }
            }
            var entry = _shopContext.Add(Product);
            // entry.CurrentValues.SetValues(StudentVM);
            await _shopContext.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}