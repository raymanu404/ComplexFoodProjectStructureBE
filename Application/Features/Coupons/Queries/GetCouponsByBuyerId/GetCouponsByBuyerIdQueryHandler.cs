using Application.Contracts.Persistence;
using Application.DtoModels.Coupon;
using MediatR;
using AutoMapper;

namespace Application.Features.Coupons.Queries.GetCouponsByBuyerId
{
    public class GetCouponsByBuyerIdQueryHandler : IRequestHandler<GetCouponsByBuyerIdQuery, List<CouponDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCouponsByBuyerIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CouponDto>> Handle(GetCouponsByBuyerIdQuery request, CancellationToken cancellationToken)
        {
            var coupons = new List<CouponDto>();
            try
            {
                var buyer = await _unitOfWork.Buyers.GetByIdAsync(request.BuyerId);
                if(buyer != null)
                {
                    var couponsByBuyerId = await _unitOfWork.Coupons.GetAllCouponsByBuyerIdAsync(buyer.Id);
                    coupons = _mapper.Map<List<CouponDto>>(couponsByBuyerId);

                }

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
               
            }

            return coupons;          
        }
    }
}
