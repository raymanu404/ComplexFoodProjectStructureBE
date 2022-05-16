using Application.Contracts.Persistence;
using Domain.Models.Shopping;
using Microsoft.EntityFrameworkCore;
using Domain.ValueObjects;
using AutoMapper;
using Application.DtoModels.Coupon;

namespace Infrastructure.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CouponRepository(ApplicationContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(Coupon coupon) => await _context.Coupons.AddAsync(coupon);

        public void Delete(Coupon coupon) =>  _context.Coupons.Remove(coupon);

        public async Task<List<CouponDto>> GetAllAsync()
        {
            var coupons = await _context.Coupons.ToListAsync();
            return _mapper.Map<List<CouponDto>>(coupons);
        }

        public async Task<List<CouponDto>?> GetAllCouponsByBuyerIdAsync(int buyerId)
        {
            var coupons = await _context.Coupons.Where(x => x.BuyerId == buyerId).ToListAsync();
            return _mapper.Map<List<CouponDto>>(coupons);
        }

        public async Task<CouponDto?> GetByUniqueCodeAsync(UniqueCode code, int buyerId)
        {
            var coupon = await _context.Coupons.Where(c => c.BuyerId == buyerId && c.Code == code).FirstOrDefaultAsync();
            return _mapper.Map<CouponDto>(coupon);
        }

    }
}
