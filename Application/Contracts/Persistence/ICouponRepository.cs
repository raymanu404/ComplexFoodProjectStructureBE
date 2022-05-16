using Domain.Models.Shopping;
using Domain.ValueObjects;
using Application.DtoModels.Coupon;

namespace Application.Contracts.Persistence
{
    public interface ICouponRepository
    {
        Task AddAsync(Coupon coupon);
        void Delete(Coupon coupon);
        Task<CouponDto?> GetByUniqueCodeAsync(UniqueCode code, int buyerId);
        Task<List<CouponDto>?> GetAllCouponsByBuyerIdAsync(int buyerId);
        Task<List<CouponDto>> GetAllAsync();
    }
}
