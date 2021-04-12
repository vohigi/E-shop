using System.Linq;
using System.Threading.Tasks;
using EShop.Data;
using EShop.Identity.Entities;
using EShop.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EShop.Pages.Management.Administration
{
    public class Feedbacks : PageModel
    {
        private readonly ShopContext _shopContext;
        public PaginatedList<FeedbackEntity> UserFeedbacks { get; set; }
        
        public Feedbacks(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        public async Task<IActionResult> OnGet(int pageSize = 6, int page = 1, bool isRead = false)
        {
            UserFeedbacks = await PaginatedList<FeedbackEntity>.CreateAsync(
                _shopContext.Feedbacks.
                    AsNoTracking().Where(x => x.IsRead.Equals(isRead)), page, pageSize);
            return Page();      
        }
    }
}