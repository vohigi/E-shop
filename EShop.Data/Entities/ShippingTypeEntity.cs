using EShop.Data.Abstractions;

namespace EShop.Data.Entities
{
    public class ShippingTypeEntity : IdentifiedEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
    public class ShippingTypeEntityConfiguration : IdentifiedEntityConfiguration<ShippingTypeEntity>
    {
    }
}