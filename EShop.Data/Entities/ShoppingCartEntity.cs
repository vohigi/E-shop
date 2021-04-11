using System.Collections.Generic;
using System.Linq;
using EShop.Data.Abstractions;

namespace EShop.Data.Entities
{
    public class ShoppingCartEntity : IdentifiedEntity
    {

        public double TotalPrice => CartItems?.Sum(x => x.TotalPrice) ?? 0f;

        public List<CartItemEntity> CartItems { get; set; }
        
    }
    public class ShoppingCartEntityConfiguration : IdentifiedEntityConfiguration<ShoppingCartEntity>
    {
    }
}