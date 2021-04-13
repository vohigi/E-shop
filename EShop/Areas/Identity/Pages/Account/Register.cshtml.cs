using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using EShop.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EShop.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Обов'язкове поле {0}")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Обов'язкове поле {0}")]
            [StringLength(100, ErrorMessage = "{0} має вміщати символів більше ніж {2} та менше ніж {1}.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "Паролі не співпадають")]
            public string ConfirmPassword { get; set; }
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            
            [StringLength(100, ErrorMessage = "{0} має вміщати символів більше ніж {2} та менше ніж {1}.", MinimumLength = 3)]
            [Display(Name = "Address")]
            public string Address { get; set; }
            
            [Required(ErrorMessage = "Обов'язкове поле {0}")]
            [StringLength(24, ErrorMessage = "{0} має вміщати символів більше ніж {2} та менше ніж {1}.", MinimumLength = 1)]
            [Display(Name = "FirstName")]
            public string FirstName { get; set; }
            
            [StringLength(24, ErrorMessage = "{0} має вміщати символів більше ніж {2} та менше ніж {1}.", MinimumLength = 1)]
            [Display(Name = "SecondName")]
            public string SecondName { get; set; }
            
            [StringLength(24, ErrorMessage = "{0} має вміщати символів більше ніж {2} та менше ніж {1}.", MinimumLength = 1)]
            [Required(ErrorMessage = "Обов'язкове поле {0}")]
            [Display(Name = "LastName")]
            public string LastName { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
