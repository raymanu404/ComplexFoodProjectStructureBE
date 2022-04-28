using Domain.Models.Ordering;
using Domain.Models.Shopping;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EnitityConfigurations
{
    public class CartEntityTypeConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> cartBuilder)
        {          
            cartBuilder
               .Property(x => x.TotalPrice)
               .HasConversion(v => v.Value, v => new Price(v))
               .HasColumnName("TotalPrice");

            cartBuilder
               .Property(x => x.Discount)
               .HasConversion(v => v.Value, v => new Discount(v))
               .HasColumnName("Discount");

            cartBuilder
               .Property(x => x.Code)
               .HasConversion(v => v.Value, v => new UniqueCode(v))
               .HasColumnName("Code");

            cartBuilder
                .HasOne(x => x.Buyer)
                .WithOne()
                .HasForeignKey<ShoppingCart>(x => x.BuyerId);

            cartBuilder
            .HasIndex(c => c.Code)
            .IsUnique();         
           
        }
    }
}
