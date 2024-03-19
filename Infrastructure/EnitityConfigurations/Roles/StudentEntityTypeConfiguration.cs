using Domain.Models.Roles;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EnitityConfigurations.Roles;
internal class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> config)
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

