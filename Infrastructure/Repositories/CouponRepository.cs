using Application.Contracts.Persistence;
using Domain.Models.Shopping;
using Microsoft.EntityFrameworkCore;
using Domain.ValueObjects;

namespace Infrastructure.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ApplicationContext _context;

        public CouponRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Coupon coupon) => await _context.Coupons.AddAsync(coupon);

        public void Delete(Coupon coupon) =>  _context.Coupons.Remove(coupon);

        public async Task<List<Coupon>> GetAllAsync() => await _context.Coupons.ToListAsync();

        public async Task<List<Coupon>?> GetAllCouponsByBuyerIdAsync(int buyerId) => await _context.Coupons.Where(x => x.BuyerId == buyerId).ToListAsync();

        public async Task<Coupon?> GetByUniqueCodeAsync(UniqueCode code, int buyerId) => await _context.Coupons.Where(c => c.BuyerId == buyerId && c.Code == code).FirstOrDefaultAsync();

    }
}
