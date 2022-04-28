using Domain.Models.Roles;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EnitityConfigurations
{
    public class AdminEntityTypeConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> adminBuilder)
        {
            adminBuilder
                .Property(x => x.Email)
                .HasConversion(v => v.Value, v => new Email(v))
                .HasColumnName("Email")
                .HasMaxLength(255);

            adminBuilder
                .HasIndex(x => x.Email)
                .IsUnique();

            adminBuilder
                .Property(x => x.Password)
                .HasConversion(v => v.Value, v => new Password(v))
                .HasColumnName("Password")
                .HasMaxLength(255);

            adminBuilder
               .Property(x => x.FirstName)
               .HasConversion(v => v.Value, v => new Name(v))
               .HasColumnName("FirstName")
               .HasMaxLength(255);

            adminBuilder
               .Property(x => x.LastName)
               .HasConversion(v => v.Value, v => new Name(v))
               .HasColumnName("LastName")
               .HasMaxLength(255);

        }
    }
}
