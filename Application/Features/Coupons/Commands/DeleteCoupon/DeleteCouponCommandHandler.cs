using Application.Contracts.Persistence;
using MediatR;
using Domain.ValueObjects;
using AutoMapper;
using Application.DtoModels.Coupon;
using Domain.Models.Shopping;

namespace Application.Features.Coupons.Commands.DeleteCoupon
{
    public class DeleteCouponCommandHandler : IRequestHandler<DeleteCouponCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteCouponCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<string> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var buyer = await _unitOfWork.Buyers.GetByIdAsync(request.BuyerId);
                if (buyer != null)
                {

                    var couponDto = await _unitOfWork.Coupons.GetByUniqueCodeAsync(new UniqueCode(request.Code), buyer.Id);
                    if(couponDto != null)
                    {
                        var couponToDelete = _mapper.Map<Coupon>(couponDto);
                        _unitOfWork.Coupons.Delete(couponToDelete);
                        await _unitOfWork.CommitAsync(cancellationToken);
                    }
                    else
                    {
                        return "Cuponul nu exista!";
                    }
                }
                else
                {
                    return "Userul nu a fost gasit!";
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Eroare la stergere cupon...";
            }    

            return "Utilizarea cuponului de reducere a fost facuta cu succes!";
        }
    }
}
