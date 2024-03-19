using Domain.Models.Ordering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.ValueObjects;

namespace Infrastructure.EnitityConfigurations.Orders;

public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> orderItemBuilder)
    {
        orderItemBuilder
            .Property(x => x.OrderItemId)
            .HasColumnName("Id");

        orderItemBuilder
            .Property(x => x.Cantity)
            .HasConversion(v => v.Value, v => new Cantity(v))
            .HasColumnName("Cantity");

        orderItemBuilder
            .Property(x => x.Price)
            .HasConversion(v => v.Value, v => new Price(v))
            .HasColumnName("Price");

        //orderItemBuilder
        //   .HasOne(orderItem => orderItem.Buyer)
        //   .WithMany(buyer => buyer.OrderItems)
        //   .HasForeignKey(orderItem => orderItem.BuyerId)
        //   .IsRequired()
        //   .OnDelete(DeleteBehavior.Cascade);

    }
}