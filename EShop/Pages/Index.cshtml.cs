using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace EShop.Pages
{
    public class IndexModel : PageModel
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
        public List<SelectListItem> BatteryCapacities { get; set; }
        [BindProperty]
        public List<SelectListItem> ScreenSizes { get; set; }
        [BindProperty]
        public List<SelectListItem> Weights { get; set; }
        [BindProperty]
        public List<string> SelectedManufacturers { get; set; }
        [BindProperty]
        public List<string> SelectedOperatingSystems { get; set; }
        [BindProperty]
        public List<int> SelectedRamList { get; set; }
        [BindProperty]
        public List<int> SelectedRomList { get; set; }
        [BindProperty]
        public int? SelectedBatteryCapacity { get; set; }
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Ціна дозволена лише в цілих числах")]
        [BindProperty]
        public int? MinPrice { get; set; }
        [BindProperty]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Ціна дозволена лише в цілих числах")]
        public int? MaxPrice { get; set; }
        [BindProperty]
        public float? SelectedScreenSize { get; set; }
        [BindProperty]
        public int? SelectedWeight { get; set; }
        
        [BindProperty]
        public bool HasNFC { get; set; }
        [BindProperty]
        public bool HasSdCardSlot { get; set; }
        
        public async Task<IActionResult> OnGet()
        {
            Manufacturers = PrepareManufacturersList();
            OperatingSystems = PrepareOperatingSystemsList();
            RamList = PrepareRamList();
            RomList = PrepareRomList();
            BatteryCapacities = PrepareBatteryCapacities();
            ScreenSizes = PrepareScreenSizes();
            Weights = PrepareWeights();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var a = SelectedManufacturers;
            var nfc = HasNFC;
            var b = SelectedRamList;
            var c = MinPrice;
            var bg = SelectedBatteryCapacity;
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
        private static List<SelectListItem> PrepareBatteryCapacities() => new List<SelectListItem>
        {
            new SelectListItem {Text = "Не обирати", Value = null},
            new SelectListItem {Text = "Від 2500 до 2999", Value = "2500"},
            new SelectListItem {Text = "Від 3000 до 3499", Value = "3000"},
            new SelectListItem {Text = "Від 3500 до 3999", Value = "3500"},
            new SelectListItem {Text = "Від 4000 до 4499", Value = "4000"},
            new SelectListItem {Text = "Від 4500 до 4999", Value = "4500"},
            new SelectListItem {Text = "Від 5000 до 5499", Value = "5000"},
            new SelectListItem {Text = "Від 5499 до 5999", Value = "5500"},
            new SelectListItem {Text = "Від 6000", Value = "6000"},
        };
        private static List<SelectListItem> PrepareScreenSizes() => new List<SelectListItem>
        {
            new SelectListItem {Text = "Не обирати", Value = null},
            new SelectListItem {Text = "4- 4,5", Value = "4"},
            new SelectListItem {Text = "4,5 - 5", Value = "4,5"},
            new SelectListItem {Text = "5 - 5,5", Value = "5"},
            new SelectListItem {Text = "5,5 - 6", Value = "5,5"},
            new SelectListItem {Text = "6 - 6,5", Value = "6"},
            new SelectListItem {Text = "6,5 - 7", Value = "6,5"},
            new SelectListItem {Text = "більше 7", Value = "7"}
        };
        
        private static List<SelectListItem> PrepareWeights() => new List<SelectListItem>
        {
            new SelectListItem {Text = "Не обирати", Value = null},
            new SelectListItem {Text = "100 і менше", Value = "50"},
            new SelectListItem {Text = "101-150", Value = "101"},
            new SelectListItem {Text = "151-200", Value = "151"},
            new SelectListItem {Text = "201-300", Value = "201"},
            new SelectListItem {Text = "301 і більше", Value = "301"}
        };
    }
}