using Domain.Models.Shopping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.ValueObjects;

namespace Infrastructure.EnitityConfigurations;

public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> productBuilder)
    {
        productBuilder
            .Property(x => x.Title)
            .HasColumnName("Title")
            .HasMaxLength(255);

        productBuilder
           .Property(x => x.Description)
           .HasColumnName("Description")
           .HasMaxLength(255);

        productBuilder
           .Property(x => x.Price)
           .HasConversion(v => v.Value, v => new Price(v))
           .HasColumnName("Price");

        productBuilder
            .Property(x => x.Image)
            .IsRequired(false);
    }
}