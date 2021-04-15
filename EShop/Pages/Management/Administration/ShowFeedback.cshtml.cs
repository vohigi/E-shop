using System;
using System.Linq;
using System.Threading.Tasks;
using EShop.Data;
using EShop.Identity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EShop.Pages.Management.Administration
{
    [Authorize(Roles = "Administration")]
    public class ShowFeedback : PageModel
    {
        private readonly ShopContext _shopContext;
        public ShowFeedback(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty]
        public FeedbackEntity Feedback { get; set; }
        
        public async Task<IActionResult> OnGet(Guid id)
        {
            var feedback = await _shopContext.Feedbacks.FirstOrDefaultAsync(x => x.Id == id);
            if (feedback != null)
            {
                Feedback = feedback;
                return Page();
            }

            StatusMessage = $"Помилка. Відгук з Id {id} не знайдено.";
            return Page();
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            if (id == Guid.Empty) return Page();
            
            var feedback = await _shopContext.Feedbacks.FindAsync(id);
            if (feedback == null) return Page();
            feedback.IsRead = !feedback.IsRead;
            await _shopContext.SaveChangesAsync();
            return await OnGet(id);
        }
    }
}