using Application.Contracts.Persistence;
using MediatR;
using Domain.Models.Shopping;
using Domain.Models.Enums;
using Domain.ValueObjects;
using Application.Components.RandomCode;


namespace Application.Features.Coupons.Commands.CreateCoupon
{
    public class CreateCouponCommandHandler : IRequestHandler<CreateCouponCommand, string>
    {

        private readonly IUnitOfWork _unitOfWork;
        
        public CreateCouponCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            string returnMessage = "";
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
                                totalPrice = request.Coupon.Amount * 10; 
                                break;
                            case TypeCoupons.TwentyProcent:
                                totalPrice = request.Coupon.Amount * 15;
                                break;
                            case TypeCoupons.ThirtyProcent:
                                totalPrice = request.Coupon.Amount * 20;
                                break;
                            default:
                                throw new Exception("Invalid Type of coupon!");
                        }

                        if (buyer.Balance.Value >= totalPrice)
                        {
                            for (var i = 0; i < request.Coupon.Amount; i++)
                            {

                                var newCoupon = new Coupon
                                {
                                    BuyerId = buyer.Id,
                                    Code = new UniqueCode(RandomCode.GetRandomCode(6)),
                                    DateCreated = DateTime.Now,
                                    Type = request.Coupon.Type
                                };
                            
                                await _unitOfWork.Coupons.AddAsync(newCoupon);
                                buyer.Coupons.Add(newCoupon);
                              
                            }

                            buyer.Balance = new Balance(buyer.Balance.Value - totalPrice);                          
                            await _unitOfWork.CommitAsync(cancellationToken);
                        }
                        else
                        {
                            returnMessage = "Insufficient funds!";
                        }
                      
                    }
                  
                }
                else
                {
                    returnMessage = "Buyer doesn't exists!";
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                returnMessage = "Error in Create Coupons";
                
            }
            return returnMessage;
            
        }

    }  

}
