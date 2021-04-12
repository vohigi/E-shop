using System;
using EShop.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Data.Entities
{
    public class ImageEntity : IdentifiedEntity
    {
        public byte[] ImageData { get; set; }
        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; }
    }

    public class ImageEntityConfiguration : IdentifiedEntityConfiguration<ImageEntity>
    {
        
    }
}