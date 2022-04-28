using Domain.Models.Roles;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EnitityConfigurations;

public class BuyerEntityTypeConfiguration : IEntityTypeConfiguration<Buyer>
{
    public void Configure(EntityTypeBuilder<Buyer> buyerConfiguration)
    {
        buyerConfiguration
              .Property(c => c.Email)
              .HasConversion(v => v.Value, v => new Email(v))
              .HasColumnName("Email")              
              .HasMaxLength(255);

        buyerConfiguration
            .HasIndex(c => c.Email)
            .IsUnique();

        buyerConfiguration
              .Property(c => c.Password)
              .HasConversion(v => v.Value, v => new Password(v))
              .HasColumnName("Password");

        buyerConfiguration
             .Property(c => c.FirstName)
             .HasConversion(v => v.Value, v => new Name(v))
             .HasColumnName("FirstName")
             .HasMaxLength(255);

        buyerConfiguration
             .Property(c => c.LastName)
             .HasConversion(v => v.Value, v => new Name(v))
             .HasColumnName("LastName")
             .HasMaxLength(255);

        buyerConfiguration
            .Property(c => c.PhoneNumber)
            .HasConversion(v => v.Value, v => new PhoneNumber(v))
            .HasColumnName("Phone")
            .HasMaxLength(14);

        buyerConfiguration
           .Property(c => c.Gender)
           .HasConversion(v => v.Value, v => new Gender(v))
           .HasColumnName("Gender");

        buyerConfiguration
           .Property(c => c.Confirmed)
           .HasColumnName("Confirmed")
           .HasDefaultValue(false);

        buyerConfiguration
          .Property(c => c.Balance)
          .HasConversion(v => v.Value, v => new Balance(v))
          .HasColumnName("Balance")
          .HasDefaultValue(new Balance(0));
    }
}