using System;
using System.Threading.Tasks;
using EShop.Data;
using EShop.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EShop.Pages.Management.Products
{
    public class Delete : PageModel
    {
        private readonly ShopContext _shopContext;

        public Delete(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        [BindProperty]
        public ProductEntity Product { get; set; }
        public string ErrorMessage { get; set; }
       

        #region snippet_OnGetAsync
        public async Task<IActionResult> OnGetAsync(Guid? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _shopContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = "Помилка видалення!";
            }

            return Page();
        }
        #endregion

        #region snippet_OnPostAsync
        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _shopContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            try
            {
                _shopContext.Products.Remove(product);
                await _shopContext.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("./Delete",
                    new { id = id, saveChangesError = true });
            }
        }
        #endregion
    }
}