using System;
using System.Collections.Generic;
using EShop.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Data.Entities
{
    public class SpecEntity : IdentifiedEntity
    {
        public string Name { get; set; }
        public string NameRu { get; set; }
        public string NameUa { get; set; }
        
        public List<SpecToItemLinkEntity> SpecToItemLinks { get; set; }
        public CategoryEntity Category { get; set; }
        public Guid CategoryId { get; set; }
        
    }
    public class SpecEntityConfiguration : IdentifiedEntityConfiguration<SpecEntity>
    {
        protected override void ConfigureProperties(EntityTypeBuilder<SpecEntity> builder)
        {
            base.ConfigureProperties(builder);
            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}