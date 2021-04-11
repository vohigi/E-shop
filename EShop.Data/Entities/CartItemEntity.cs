using System;
using EShop.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Data.Entities
{
    public class CartItemEntity : IdentifiedEntity
    {
        public ItemEntity Item { get; set; }
        public Guid? ItemId { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice => Item.Price * Quantity;
        
    }
    public class CartItemEntityConfiguration : IdentifiedEntityConfiguration<CartItemEntity>
    {
        protected override void ConfigureProperties(EntityTypeBuilder<CartItemEntity> builder)
        {
            base.ConfigureProperties(builder);
            builder.HasOne(x => x.Item)
                .WithMany()
                .HasForeignKey(x => x.ItemId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}