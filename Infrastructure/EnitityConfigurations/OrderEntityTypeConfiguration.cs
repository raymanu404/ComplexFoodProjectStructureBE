using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Ordering;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.ValueObjects;

namespace Infrastructure.EnitityConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> orderBuilder)
        {

            orderBuilder
                .Property(x => x.TotalPrice)
                .HasConversion(v => v.Value, v => new Price(v))
                .HasColumnName("TotalPrice");

            orderBuilder
                .Property(x => x.Discount)
                .HasConversion(v => v.Value, v => new Discount(v))
                .HasColumnName("Discount");

            orderBuilder
                .Property(x => x.Code)
                .HasConversion(v => v.Value, v => new UniqueCode(v))
                .HasColumnName("Code")
                .HasMaxLength(6);

            orderBuilder
               .HasIndex(c => c.Code)
               .IsUnique();

            
        }
    }
}
