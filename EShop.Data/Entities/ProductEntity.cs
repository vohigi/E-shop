using System.Collections.Generic;
using EShop.Data.Abstractions;
using EShop.Data.Enums;
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
        
        public string Manufacturer { get; set; }
        public string OperatingSystem { get; set; }
        public int? Ram { get; set; }
        public int? Rom { get; set; }
        public int? NumberOfSimCards  { get; set; }
        public bool HasSdCardSlot { get; set; }
        public string ProcessorName { get; set; }
        public int? NumberOfCores { get; set; }
        public float? Clock { get; set; }
        public int? BatteryCapacity { get; set; }
        public float? DisplayDiagonal { get; set; }
        public int? DisplayWidth { get; set; }
        public int? DisplayHeight { get; set; }
        public DisplayType? DisplayType { get; set; }
        public string RearCamera { get; set; }
        public string FrontCamera { get; set; }
        public bool HasNFC { get; set; }
        public float? Width { get; set; }
        public float? Height { get; set; }
        public float? Thickness { get; set; }
        public int? Weight { get; set; }
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