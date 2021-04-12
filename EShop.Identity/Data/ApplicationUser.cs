using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EShop.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string AddressText { get; set; }
    }
}