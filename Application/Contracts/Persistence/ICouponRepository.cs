using Domain.Models.Shopping;
using Domain.ValueObjects;

namespace Application.Contracts.Persistence
{
    public interface ICouponRepository
    {
        Task AddAsync(Coupon coupon);
        void Delete(Coupon coupon);
        Task<Coupon?> GetByUniqueCodeAsync(UniqueCode code, int buyerId);
        Task<List<Coupon>?> GetAllCouponsByBuyerIdAsync(int buyerId);
        Task<List<Coupon>> GetAllAsync();
    }
}
