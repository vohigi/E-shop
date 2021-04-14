using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EShop.Data;
using EShop.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EShop.Pages.Customer
{
    public class PaymentSystem : PageModel
    {
        private readonly ShopContext _shopContext;
        public PaymentSystem(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        [BindProperty]
        public PaymentDetailsModel PaymentDetails { get; set; }
        public class PaymentDetailsModel
        {
            [Required(ErrorMessage = "Обов'язкове поле {0}")]
            public string UserName { get; set; }
            [Required(ErrorMessage = "Обов'язкове поле {0}")]
            [RegularExpression("^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1-7][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\\d{3})\\d{11})$", ErrorMessage = "Введено невірний номер карти")]
            public string CardNumber { get; set; }
            [Required(ErrorMessage = "Обов'язкове поле {0}")]
            public int Month { get; set; }
            [Required(ErrorMessage = "Обов'язкове поле {0}")]
            public int Year { get; set; }
            [Required(ErrorMessage = "Обов'язкове поле {0}")]
            public int CVV { get; set; }
        }
        
        public OrderEntity Order { get; set; }
        
        public async Task<IActionResult> OnGet(Guid id)
        {
            Order = await _shopContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
            return Page();
        }

        public async Task<IActionResult> OnPost(Guid? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var order = await _shopContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (order != null)
            {
                order.IsPaid = true;
                await _shopContext.SaveChangesAsync();
                return RedirectToPage("/Order/Details", new {id = order.Id, justPaid = true});
            }
            return Page();
        }
    }
}