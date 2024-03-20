using Domain.Models.Roles;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.EntityConfigurations.Roles;

internal class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> config)
    {
        config.Ignore(x => x.Id);
        config.Ignore(x => x.Password);
        config.Ignore(x => x.FirstName);
        config.Ignore(x => x.LastName);

        config
            .Property(c => c.Email)
            .HasConversion(v => v.Value, v => new Email(v))
            .HasColumnName("Email")
            .HasMaxLength(255);

        config
            .HasIndex(c => c.Email)
            .IsUnique();

        config
            .Property(c => c.AcademicYear)
            .HasConversion(v => v.Value, v => new AcademicYear(v))
            .HasColumnName("AcademicYear");

        config
            .Property(c => c.MatrNumber)
            .HasConversion(v => v.Value, v => new MatrNumber(v))
            .HasColumnName("MatrNumber");

        config.HasOne(student => student.Buyer)
            .WithOne(b => b.Student)
            .HasForeignKey<Student>(f => f.BuyerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}