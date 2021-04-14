using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Data.Abstractions;

namespace EShop.Data.Entities
{
    public class ShoppingCartEntity : IdentifiedEntity
    {
        public string CustomerId { get; set; }
        
        public Guid? OrderId { get; set; }
        public double TotalPrice => CartItems?.Sum(x => x.TotalPrice) ?? 0f;
        public OrderEntity Order { get; set; }
        public List<CartItemEntity> CartItems { get; set; }
        
    }
    public class ShoppingCartEntityConfiguration : IdentifiedEntityConfiguration<ShoppingCartEntity>
    {
    }
}