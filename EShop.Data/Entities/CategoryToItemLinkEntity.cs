using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Data.Entities
{
    public class CategoryToItemLinkEntity
    {
        public Guid ItemId { get; set; }
        public ProductEntity Item { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryEntity Category { get; set; }
    }
    public class CategoryToItemLinkEntityConfiguration : IEntityTypeConfiguration<CategoryToItemLinkEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryToItemLinkEntity> builder)
        {
            builder.HasKey(x => new {x.ItemId, x.CategoryId});
            builder.HasOne(x => x.Item)
                .WithMany(x => x.CategoryToItemLinks)
                .HasForeignKey(x => x.ItemId)
                .HasPrincipalKey(x=>x.Id);
            
            builder.HasOne(x => x.Category)
                .WithMany(x => x.CategoryToItemLinks)
                .HasForeignKey(x => x.CategoryId)
                .HasPrincipalKey(x=>x.Id);
        }
    }
}