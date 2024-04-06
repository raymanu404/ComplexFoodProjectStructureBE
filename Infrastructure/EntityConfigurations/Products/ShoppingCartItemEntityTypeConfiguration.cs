using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.ValueObjects;
using Domain.Models.Shopping;

namespace Infrastructure.EntityConfigurations.Products;

public class ShoppingCartItemEntityTypeConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
{
    public void Configure(EntityTypeBuilder<ShoppingCartItem> shoppingCartItemBuilder)
    {
        shoppingCartItemBuilder
           .Property(c => c.Cantity)
           .HasConversion(v => v.Value, v => new Cantity(v))
           .HasColumnName("Cantity");

        shoppingCartItemBuilder
            .HasKey(x => new { x.ProductId, x.ShoppingCartId });

    }
}