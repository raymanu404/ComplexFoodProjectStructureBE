using Domain.Models.Ordering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.ValueObjects;
using Domain.Models.Shopping;

namespace Infrastructure.EntityConfigurations.Orders;

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

        orderItemBuilder
            .HasOne(x => x.Product)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.ProductId);
       
    }
}