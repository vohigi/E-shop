using System.Collections.Generic;
using EShop.Data.Abstractions;

namespace EShop.Data.Entities
{
    public class CategoryEntity : IdentifiedEntity
    {
        public string Name { get; set; }
        public List<CategoryToItemLinkEntity> CategoryToItemLinks { get; set; }

    }
    public class CategoryEntityConfiguration : IdentifiedEntityConfiguration<CategoryEntity>
    {
    }
}