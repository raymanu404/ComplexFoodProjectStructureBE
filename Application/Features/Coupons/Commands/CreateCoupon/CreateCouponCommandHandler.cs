using Application.Contracts.Persistence;
using MediatR;
using Domain.Models.Shopping;
using AutoMapper;
using Domain.Models.Enums;
using Domain.ValueObjects;
using Application.DtoModels.Coupon;

namespace Application.Features.Coupons.Commands.CreateCoupon
{
    public class CreateCouponCommandHandler : IRequestHandler<CreateCouponCommand, List<CouponDto>>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public CreateCouponCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<CouponDto>> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            var coupons = new List<CouponDto>();
            try
            {
                var buyer = await _unitOfWork.Buyers.GetByIdAsync(request.BuyerId);
                if (buyer != null)
                {
                    if (buyer.Balance.Value > 0)
                    {
                        var totalPrice = 0;
                        switch (request.Coupon.Type)
                        {
                            case TypeCoupons.TenProcent:
                                totalPrice = request.Coupon.Amount.Value * 10; 
                                break;
                            case TypeCoupons.TwentyProcent:
                                totalPrice = request.Coupon.Amount.Value * 15;
                                break;
                            case TypeCoupons.ThirtyProcent:
                                totalPrice = request.Coupon.Amount.Value * 20;
                                break;
                            default:
                                throw new Exception("Invalid Type of coupon!");
                        }

                        if (buyer.Balance.Value >= totalPrice)
                        {
                            for (var i = 0; i < request.Coupon.Amount.Value; i++)
                            {
                                var newCouponDto = new CouponDto
                                {
                                    BuyerId = buyer.Id,
                                    Code = new UniqueCode(RandomCode(6)),
                                    DateCreated = DateTime.Now,
                                    Type = request.Coupon.Type
                                };

                                var coupon = _mapper.Map<Coupon>(newCouponDto);
                                await _unitOfWork.Coupons.AddAsync(coupon);
                                buyer.Coupons.Add(coupon);

                                coupons.Add(newCouponDto);
                              
                            }

                            buyer.Balance = new Balance(buyer.Balance.Value - totalPrice);                          
                            await _unitOfWork.CommitAsync(cancellationToken);
                        }
                        else
                        {
                            
                        }
                      
                    }
                  
                }
                else
                {
                   
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            return coupons;
            
        }

        private static string RandomCode(int length)
        {
            var ran = new Random();

            var b = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            string random = "";

            for (var i = 0; i < length; i++)
            {
                var a = ran.Next(b.Length);
                random = random + b.ElementAt(a);
            }

            return random;
        }
    }  

}
