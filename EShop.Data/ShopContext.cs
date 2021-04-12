using EShop.Data.Entities;
using EShop.Identity.Entities;
using Microsoft.EntityFrameworkCore;

namespace EShop.Data
{
    public class ShopContext : DbContext
    {
        public DbSet<ItemEntity> Items { get; set; }
        public DbSet<CartItemEntity> CartItems { get; set; }
        public DbSet<ShoppingCartEntity> ShoppingCarts { get; set; }
        public DbSet<SpecEntity> Specs { get; set; }
        public DbSet<SpecToItemLinkEntity> SpecToItemLinks { get; set; }
        public DbSet<ShippingTypeEntity> ShippingTypes { get; set; }
        public DbSet<PaymentTypeEntity> PaymentTypes { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<CategoryToItemLinkEntity> CategoryToItemLinks { get; set; }
        public DbSet<FeedbackEntity> Feedbacks { get; set; }
        
        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseIdentityColumns();

            modelBuilder.ApplyConfiguration(new CategoryToItemLinkEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SpecToItemLinkEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FeedbackEntityConfiguration());
        }
    }
}