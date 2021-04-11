using EShop.Data.Abstractions;

namespace EShop.Data.Entities
{
    public class PaymentTypeEntity : IdentifiedEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class PaymentTypeEntityConfiguration : IdentifiedEntityConfiguration<PaymentTypeEntity>
    {
    }
}