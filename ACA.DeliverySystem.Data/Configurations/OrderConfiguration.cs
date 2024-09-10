using ACA.DeliverySystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACA.DeliverySystem.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();

            builder.HasMany(x => x.OrderItems)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
