using Domain.Models.Shopping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.ValueObjects;
namespace Infrastructure.EnitityConfigurations;

public class CouponEntityTypeConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> couponBuilder)
    {

        couponBuilder
           .Property(x => x.Code)
           .HasConversion(v => v.Value, v => new UniqueCode(v))
           .HasColumnName("Code")
           .HasMaxLength(6);

        couponBuilder
           .HasIndex(c => c.Code)
           .IsUnique();

        couponBuilder
            .HasOne(coupon => coupon.Buyer)
            .WithMany(buyer => buyer.Coupons)
            .HasForeignKey(coupon => coupon.BuyerId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
    }
}