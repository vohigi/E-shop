using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShop.Pages.Shop
{
    public class MainPage : PageModel
    {
        [BindProperty]
        public List<SelectListItem> Manufacturers { get; set; }
        [BindProperty]
        public List<SelectListItem> OperatingSystems { get; set; }
        [BindProperty]
        public List<string> SelectedManufacturers { get; set; }
        [BindProperty]
        public List<string> SelectedOperatingSystems { get; set; }
        [BindProperty]
        public bool HasNFC { get; set; }
        
        public async Task<IActionResult> OnGet()
        {
            Manufacturers = new List<SelectListItem>
            {
                new SelectListItem {Text = "Apple", Value = "Apple"},
                new SelectListItem {Text = "OnePlus", Value = "OnePlus"},
                new SelectListItem {Text = "Xiaomi", Value = "Xiaomi"}
            };
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var a = SelectedManufacturers;
            var nfc = HasNFC;
            return Page();
        }
        
    }
}