using System;
using EShop.Data.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Identity.Entities
{
    public class FeedbackEntity : IdentifiedEntity
    {
        public string CustomerEmail { get; set; }
        public string CustomerName { get; set; }
        public string MessageHeader { get; set; }
        public string MessageText { get; set; }
        public bool IsRead { get; set; }
    }
    
    public class FeedbackEntityConfiguration : IdentifiedEntityConfiguration<FeedbackEntity>
    {
        protected override void ConfigureProperties(EntityTypeBuilder<FeedbackEntity> builder)
        {
            base.ConfigureProperties(builder);
            builder.HasIndex(x => x.IsRead);
        }
    } 
}