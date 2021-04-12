using System;
using EShop.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Data.Entities
{
    public class OrderEntity : IdentifiedEntity
    {
        public Guid CustomerId { get; set; }
        public PaymentTypeEntity PaymentType { get; set; }
        public Guid? PaymentTypeId { get; set; }
        public ShippingTypeEntity ShippingType { get; set; }
        public Guid? ShippingTypeId { get; set; }
        public double TotalPrice { get; set; }
        public ShoppingCartEntity ShoppingCart { get; set; }
        public Guid ShoppingCartId { get; set; }
    }
    public class OrderEntityConfiguration : IdentifiedEntityConfiguration<OrderEntity>
    {
        protected override void ConfigureProperties(EntityTypeBuilder<OrderEntity> builder)
        {
            base.ConfigureProperties(builder);
            builder.HasOne(x => x.ShippingType)
                .WithMany()
                .HasForeignKey(x => x.ShippingTypeId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.PaymentType)
                .WithMany()
                .HasForeignKey(x => x.PaymentTypeId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.ShoppingCart)
                .WithMany()
                .HasForeignKey(x => x.ShoppingCartId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    } 
}