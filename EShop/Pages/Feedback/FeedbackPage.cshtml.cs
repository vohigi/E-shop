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
        
        [BindProperty]
        public FeedbackEntity Feedback { get; set; }
        [TempData]
        public string StatusMessage { get; set; }
        
        public void OnGet()
        {
            StatusMessage = null;
            if (Feedback == null) return;
            Feedback.MessageHeader = "";
            Feedback.MessageText = "";
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            Feedback.CustomerEmail = user.UserName;
            Feedback.IsRead = false;
            Feedback.CustomerName = TextHelper.ConvertToFullName(user.FirstName, user.SecondName, user.LastName);
            if (!ModelState.IsValid)
            {
                StatusMessage = "Помилка. Неможливо надіслати форму. Будь ласка, заповніть всі поля";
                return Page();
            }

            await using var context = _context;
            await context.Feedbacks.AddAsync(Feedback);
            await context.SaveChangesAsync();
            StatusMessage = "Дякуємо! Ваше звернення успішно збережене";
            return Page();
        }
    }
}