using System;
using System.Linq;
using EShop.Data;
using Microsoft.AspNetCore.Identity;

namespace EShop.Identity
{
    public static class RoleInitializer
    {
        private const string adminEmail = "administrator@gmail.com";
        private const string administrationRoleName = "Administration";
        
        public static void AdministratorInitialize(IdentityContext identityContext)
        {
            using var context = identityContext;
            
            context.Database.EnsureCreated();
            
            //stop initializing if administrator role already exists
            if(context.Roles.Any(b => b.NormalizedName.Equals(administrationRoleName.ToUpper()))) return;
            
            //create role
            var roleId = Guid.NewGuid().ToString();
            context.Roles.Add(new IdentityRole
            {
                Id = roleId,
                Name = administrationRoleName,
                NormalizedName = administrationRoleName.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });
            //create user for that role 
            var userId = Guid.NewGuid().ToString();
            context.Users.Add(new ApplicationUser
            {
                Id = userId,
                UserName = adminEmail.ToLower(),
                NormalizedUserName = adminEmail.ToUpper(),
                EmailConfirmed = true,
                //password is "Admin_1503"
                PasswordHash = "AQAAAAEAACcQAAAAEGGGxHhZ9YK4uX8OXMqgpQ4Cl6Lwv3itufw85+iO7FhZoB+wVwU5EcIiSoWwGoDBeQ==",
                SecurityStamp = "S4K6NE22SNZQPJKB7NGWWZ3VED6XJECF",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = false
            });
            
            //link user to role
            context.UserRoles.Add(new IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = userId
            });
            
            context.SaveChanges();
        }
    }
}