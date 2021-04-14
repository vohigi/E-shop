using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EShop.Data;
using EShop.Data.Entities;
using EShop.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EShop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ShopContext _shopContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public IndexModel(ShopContext shopContext, UserManager<ApplicationUser> userManager)
        {
            _shopContext = shopContext;
            _userManager = userManager;
        }
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
        public double? MinPrice { get; set; }
        [BindProperty]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Ціна дозволена лише в цілих числах")]
        public double? MaxPrice { get; set; }
        [BindProperty]
        public float? SelectedScreenSize { get; set; }
        [BindProperty]
        public int? SelectedWeight { get; set; }
        
        [BindProperty]
        public bool HasNFC { get; set; }
        [BindProperty]
        public bool HasSdCardSlot { get; set; }
        [BindProperty]
        [StringLength(30, ErrorMessage = "{0} має вміщати символів більше ніж {2} та менше ніж {1}.", MinimumLength = 3)]
        public string SearchString { get; set; }
        public PaginatedList<ProductEntity> PaginatedProductsList { get; set; }
        
        private List<ProductEntity> ProductList { get; set; }
        
        public async Task<IActionResult> OnGet(int pageSize = 6, int page = 1)
        {
            PaginatedProductsList = await PaginatedList<ProductEntity>.CreateAsync(GetProductEntities(), page, pageSize);

            Manufacturers = PrepareManufacturersList();
            OperatingSystems = PrepareOperatingSystemsList();
            RamList = PrepareRamList();
            RomList = PrepareRomList();
            BatteryCapacities = PrepareBatteryCapacities();
            ScreenSizes = PrepareScreenSizes();
            Weights = PrepareWeights();
            HasNFC = false;
            HasSdCardSlot = false;
            return Page();
        }
        public async Task<IActionResult> OnPostAddToCart(Guid? id)
        {
            var userId = _userManager.GetUserId(Request.HttpContext.User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login");
            }

            var product = await _shopContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            var existingShoppingCart =
                await _shopContext.ShoppingCarts.Include(x => x.CartItems).ThenInclude(x => x.Item)
                    .ThenInclude(x => x.Images).FirstOrDefaultAsync(
                        x =>
                            x.CustomerId == userId && !x.OrderId.HasValue);
            var existingCartItem = existingShoppingCart.CartItems?.FirstOrDefault(x => x.ItemId == id);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
                _shopContext.CartItems.Update(existingCartItem);
                await _shopContext.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            if (existingShoppingCart.CartItems == null)
            {
                existingShoppingCart.CartItems = new List<CartItemEntity>();
            }
            existingShoppingCart.CartItems.Add(new CartItemEntity()
            {
                Item = product,
                Quantity = 1
            });
            _shopContext.ShoppingCarts.Update(existingShoppingCart);
            await _shopContext.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostSearchText()
        {
            
            return await OnGet();
        }
        
        public async Task<IActionResult> OnPostSearch()
        {
            return await OnGet();
        }

        /*private void SetBindsToNull()
        {
            return await OnGet();
        }*/
        private IQueryable<ProductEntity> GetProductEntities()
        {
            var result = _shopContext.Products
                .AsNoTracking()
                .Include(i => i.Images)
                .Where(p =>
                    #region search string adapter
                    (string.IsNullOrEmpty(SearchString) || SearchString.Length < 3 
                     || p.DisplayName.ToLower().Contains(SearchString.ToLower()) 
                     || p.Manufacturer.ToLower().Contains(SearchString.ToLower()))
                    #endregion
                    &&
                    #region manufacturer adapter
                    (SelectedManufacturers == null || !SelectedManufacturers.Any()
                    || SelectedManufacturers.Contains(p.Manufacturer.ToLower()))
                    #endregion
                    &&
                    #region OS adapter
                    (SelectedOperatingSystems == null || !SelectedOperatingSystems.Any()
                    || SelectedOperatingSystems.Contains(p.OperatingSystem.ToLower()))
                    #endregion
                    &&
                    #region RAM adapter
                    (!p.Ram.HasValue || SelectedRamList == null || !SelectedRamList.Any()
                    || SelectedRamList.Contains(p.Ram.Value))
                    #endregion
                    &&
                    #region ROM adapter
                    (!p.Rom.HasValue || SelectedRomList == null || !SelectedRomList.Any()
                     || SelectedRomList.Contains(p.Rom.Value))
                    #endregion
                    &&
                    #region battery capacity adapter
                    (!SelectedBatteryCapacity.HasValue || SelectedBatteryCapacity < 2500
                     || SelectedBatteryCapacity >= 6000 && p.BatteryCapacity >= 6000
                     || p.BatteryCapacity >= SelectedBatteryCapacity && p.BatteryCapacity < SelectedBatteryCapacity + 500)
                    #endregion
                    &&
                    #region price adapter
                    (!MinPrice.HasValue && !MaxPrice.HasValue ||
                     !MinPrice.HasValue && MaxPrice.HasValue && p.Price <= MaxPrice ||
                     !MaxPrice.HasValue && MinPrice.HasValue && p.Price >= MinPrice ||
                     MinPrice.HasValue && MaxPrice.HasValue && p.Price >= MinPrice && p.Price <= MaxPrice)
                    #endregion
                    &&
                    #region display diagonal adapter
                    (!SelectedScreenSize.HasValue || SelectedScreenSize < 4
                     || SelectedScreenSize >= 7 && p.DisplayDiagonal >= 7
                     || p.DisplayDiagonal >= SelectedScreenSize && p.DisplayDiagonal <= SelectedScreenSize + 0.5)
                    #endregion
                    &&
                    #region weight adapter
                    (!SelectedWeight.HasValue || SelectedWeight <= 100 && p.Weight <= 100
                     || SelectedWeight > 300 && p.Weight > 300
                     || p.Weight >= SelectedWeight && p.Weight < SelectedWeight + 50)
                    #endregion
                    &&
                    #region additonal filters
                    (!HasNFC || p.HasNFC)
                    &&
                    (!HasSdCardSlot || p.HasSdCardSlot)
                    #endregion
                )
                .OrderByDescending(p => p.Price)
                .ThenByDescending(p => p.Manufacturer);
            return result;
        }

        private static List<SelectListItem> PrepareManufacturersList() => new List<SelectListItem>
        {
            //contain only lowercase values
            new SelectListItem {Text = "Apple", Value = "apple"},
            new SelectListItem {Text = "OnePlus", Value = "oneplus"},
            new SelectListItem {Text = "Xiaomi", Value = "xiaomi"}
        };
        private static List<SelectListItem> PrepareOperatingSystemsList() => new List<SelectListItem>
        {
            new SelectListItem {Text = "Android", Value = "android"},
            new SelectListItem {Text = "IOS", Value = "ios"}
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
            new SelectListItem {Text = "201-250", Value = "201"},
            new SelectListItem {Text = "251-300", Value = "201"},
            new SelectListItem {Text = "301 і більше", Value = "301"}
        };
    }
}