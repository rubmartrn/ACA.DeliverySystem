using ACA.DeliverySystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACA.DeliverySystem.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(oi => oi.Item)
                .WithMany()
                .HasForeignKey(oi => oi.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Order)
           .WithMany(o => o.OrderItems)
           .HasForeignKey(x => x.OrderId)
           .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
