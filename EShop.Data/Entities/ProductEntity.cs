using System.Collections.Generic;
using EShop.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Data.Entities
{
    public class ProductEntity : IdentifiedEntity
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public List<CategoryToItemLinkEntity> CategoryToItemLinks { get; set; }
        public List<SpecToItemLinkEntity> SpecToItemLinks { get; set; }
        
        public List<ImageEntity> Images { get; set; }
          
    }
    public class ProductEntityConfiguration : IdentifiedEntityConfiguration<ProductEntity>
    {
        protected override void ConfigureProperties(EntityTypeBuilder<ProductEntity> builder)
        {
            base.ConfigureProperties(builder);
            builder.HasMany(x => x.Images)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}