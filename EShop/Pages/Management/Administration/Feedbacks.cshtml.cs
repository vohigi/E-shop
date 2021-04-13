using System;
using System.Linq;
using System.Threading.Tasks;
using EShop.Data;
using EShop.Data.Helpers;
using EShop.Identity.Entities;
using EShop.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EShop.Pages.Management.Administration
{
    public class FeedbacksModel : PageModel
    {
        private readonly ShopContext _shopContext;
        public PaginatedList<FeedbackEntity> UserFeedbacks { get; set; }
        
        public FeedbacksModel(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList IsReadValues { get; set; }
        [BindProperty(SupportsGet = true)] 
        public string IsReadValue { get; set; }
        public async Task<IActionResult> OnGet(int pageSize = 6, int page = 1)
        {
            var isRead = !string.IsNullOrEmpty(IsReadValue) && IsReadValue.Contains("true");
            UserFeedbacks = await PaginatedList<FeedbackEntity>.CreateAsync(
                _shopContext.Feedbacks.
                    AsNoTracking()
                    .Where(
                        x => 
                            x.IsRead.Equals(isRead) 
                            && string.IsNullOrEmpty(SearchString) 
                             || x.CustomerEmail.Contains(SearchString) && x.IsRead.Equals(isRead)) 
                , page, pageSize);
            foreach (var feedbackEntity in UserFeedbacks)
            {
                feedbackEntity.MessageHeader = TextHelper.GetShortString(feedbackEntity.MessageHeader);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostIsReadAsync(Guid id)
        {
            await using var context = _shopContext;
            var feedback = await context.Feedbacks.FindAsync(id);
            if (feedback != null)
            {
                feedback.IsRead = !feedback.IsRead;
                await context.SaveChangesAsync();
                StatusMessage = $"Відгук користувача {feedback.CustomerEmail} позначений {(!feedback.IsRead ? "непрочитаним" : "прочитаним")}";
            }
            else
            {
                StatusMessage = $"Помилка. Відгук користувача {feedback.CustomerEmail} із Id {id} не знайдено";
            }

            return await OnGet();
        }
    }
}