using Domain.Models.Roles;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.EntityConfigurations.Roles;
internal class AdminEntityTypeConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> config)
    {
        config
              .Property(c => c.Email)
              .HasConversion(v => v.Value, v => new Email(v))
              .HasColumnName("Email")
              .HasMaxLength(255);

        config
            .HasIndex(c => c.Email)
            .IsUnique();

        config
              .Property(c => c.Password)
              .HasConversion(v => v.Value, v => new Password(v))
              .HasColumnName("Password");

        config
             .Property(c => c.FirstName)
             .HasConversion(v => v.Value, v => new Name(v))
             .HasColumnName("FirstName")
             .HasMaxLength(255);

        config
             .Property(c => c.LastName)
             .HasConversion(v => v.Value, v => new Name(v))
             .HasColumnName("LastName")
             .HasMaxLength(255);

    }
}

