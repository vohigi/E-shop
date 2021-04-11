using System.Collections.Generic;
using EShop.Data.Abstractions;

namespace EShop.Data.Entities
{
    public class ItemEntity : IdentifiedEntity
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<CategoryToItemLinkEntity> CategoryToItemLinks { get; set; }
        public List<SpecToItemLinkEntity> SpecToItemLinks { get; set; }
        
          
    }
    public class ItemEntityConfiguration : IdentifiedEntityConfiguration<ItemEntity>
    {
    } 
}