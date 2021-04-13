using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EShop.Data;
using EShop.Data.Helpers;
using EShop.Identity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.Pages.Feedback
{
    [Authorize]
    public class FeedbackPage : PageModel
    {
        private readonly ShopContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        

        public FeedbackPage(ShopContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        
        private FeedbackEntity Feedback { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required(ErrorMessage = "Обов'язкове поле {0}")]
            [StringLength(100, ErrorMessage = "{0} має вміщати символів більше ніж {2} та менше ніж {1}.", MinimumLength = 1)]
            public string MessageHeader { get; set; }
            [Required(ErrorMessage = "Обов'язкове поле {0}")]
            [StringLength(500, ErrorMessage = "{0} має вміщати символів більше ніж {2} та менше ніж {1}.", MinimumLength = 20)]
            public string MessageText { get; set; }
        } 
        
        [TempData]
        public string StatusMessage { get; set; }
        
        public void OnGet()
        {
            StatusMessage = null;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                StatusMessage = "Помилка. Неможливо надіслати форму. Будь ласка, заповніть всі поля";
                return Page();
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Feedback = new FeedbackEntity
            {
                CustomerEmail = user.UserName,
                IsRead = false,
                CustomerName = TextHelper.ConvertToFullName(user.FirstName, user.SecondName, user.LastName),
                MessageHeader = Input.MessageHeader,
                MessageText = Input.MessageText
            };
            await using var context = _context;
            await context.Feedbacks.AddAsync(Feedback);
            await context.SaveChangesAsync();
            StatusMessage = "Дякуємо! Ваше звернення успішно збережене";
            return Page();
        }
    }
}