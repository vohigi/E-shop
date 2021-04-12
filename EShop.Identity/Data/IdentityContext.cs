using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EShop.Data
{
    public class IdentityContext : IdentityDbContext
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
            
        }
    }
}