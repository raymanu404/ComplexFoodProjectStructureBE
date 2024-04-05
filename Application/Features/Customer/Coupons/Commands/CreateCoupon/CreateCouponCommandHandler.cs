using Application.Components;
using MediatR;
using Domain.Models.Shopping;
using Domain.Models.Enums;
using Domain.ValueObjects;
using Application.Contracts.Persistence.Customer;


namespace Application.Features.Customer.Coupons.Commands.CreateCoupon
{

    public class CreateCouponCommandHandler : IRequestHandler<CreateCouponCommand, string>
    {

        const int PRICE_TICKET_TYPE1 = 10;
        const int PRICE_TICKET_TYPE2 = 30;
        const int PRICE_TICKET_TYPE3 = 50;

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

                    var totalPrice = 0;
                    var amountOfCoupons = 0;
                    switch (request.Coupon.Type)
                    {
                        case TypeCoupons.TenProcent:
                            totalPrice = PRICE_TICKET_TYPE1;
                            amountOfCoupons = 1;
                            break;
                        case TypeCoupons.TwentyProcent:
                            totalPrice = PRICE_TICKET_TYPE2;
                            amountOfCoupons = 3;
                            break;
                        case TypeCoupons.ThirtyProcent:
                            totalPrice = PRICE_TICKET_TYPE3;
                            amountOfCoupons = 5;
                            break;
                        default:
                            throw new Exception("Invalid Type of coupon!");
                    }

                    if (buyer.Balance.Value >= totalPrice)
                    {
                        for (var i = 0; i < amountOfCoupons; i++)
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
                        return $"Successfully!{buyer.Balance.Value}";
                    }
                    else
                    {
                        returnMessage = "Insufficient funds!";
                    }

                }
                else
                {
                    returnMessage = "Buyer doesn't exists!";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                returnMessage = "Error in Create Coupons";

            }
            return returnMessage;

        }

    }

}
