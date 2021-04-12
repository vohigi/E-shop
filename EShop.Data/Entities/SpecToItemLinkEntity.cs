using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Data.Entities
{
    public class SpecToItemLinkEntity
    {
        public Guid ItemId { get; set; }
        public ProductEntity Item { get; set; }
        public Guid SpecId { get; set; }
        public SpecEntity Spec { get; set; }
        
        public string Value { get; set; }
    }
    public class SpecToItemLinkEntityConfiguration : IEntityTypeConfiguration<SpecToItemLinkEntity>
    {
        public void Configure(EntityTypeBuilder<SpecToItemLinkEntity> builder)
        {
            builder.HasKey(x => new {x.ItemId, x.SpecId});
            builder.HasOne(x => x.Item)
                .WithMany(x => x.SpecToItemLinks)
                .HasForeignKey(x => x.ItemId)
                .HasPrincipalKey(x=>x.Id);
            
            builder.HasOne(x => x.Spec)
                .WithMany(x => x.SpecToItemLinks)
                .HasForeignKey(x => x.SpecId)
                .HasPrincipalKey(x=>x.Id);
        }
    }
}