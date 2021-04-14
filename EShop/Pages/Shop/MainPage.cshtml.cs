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
        public List<SelectListItem> RamList { get; set; }
        [BindProperty]
        public List<SelectListItem> RomList { get; set; }
        [BindProperty]
        public List<string> SelectedManufacturers { get; set; }
        [BindProperty]
        public List<string> SelectedOperatingSystems { get; set; }
        [BindProperty]
        public List<string> SelectedRamList { get; set; }
        [BindProperty]
        public List<string> SelectedRomList { get; set; }
        [BindProperty]
        public bool HasNFC { get; set; }
        
        public async Task<IActionResult> OnGet()
        {
            Manufacturers = PrepareManufacturersList();
            OperatingSystems = PrepareOperatingSystemsList();
            RamList = PrepareRamList();
            RomList = PrepareRomList();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var a = SelectedManufacturers;
            var nfc = HasNFC;
            return await OnGet();
        }

        private static List<SelectListItem> PrepareManufacturersList() => new List<SelectListItem>
        {
            new SelectListItem {Text = "Apple", Value = "Apple"},
            new SelectListItem {Text = "OnePlus", Value = "OnePlus"},
            new SelectListItem {Text = "Xiaomi", Value = "Xiaomi"}
        };
        private static List<SelectListItem> PrepareOperatingSystemsList() => new List<SelectListItem>
        {
            new SelectListItem {Text = "Android", Value = "Android"},
            new SelectListItem {Text = "IOS", Value = "IOS"}
        };

        private static List<SelectListItem> PrepareRamList() => new List<SelectListItem>
        {
            new SelectListItem {Text = "1", Value = "1"},
            new SelectListItem {Text = "2", Value = "2"},
            new SelectListItem {Text = "3", Value = "3"},
            new SelectListItem {Text = "4", Value = "4"},
            new SelectListItem {Text = "6", Value = "6"},
            new SelectListItem {Text = "8", Value = "8"},
            new SelectListItem {Text = "10", Value = "10"},
            new SelectListItem {Text = "12", Value = "12"},
            new SelectListItem {Text = "16", Value = "16"}
        };
        private static List<SelectListItem> PrepareRomList() => new List<SelectListItem>
        {
            new SelectListItem {Text = "8", Value = "8"},
            new SelectListItem {Text = "16", Value = "16"},
            new SelectListItem {Text = "32", Value = "32"},
            new SelectListItem {Text = "64", Value = "64"},
            new SelectListItem {Text = "128", Value = "128"},
            new SelectListItem {Text = "256", Value = "256"},
            new SelectListItem {Text = "512", Value = "512"}
        };
    }
}