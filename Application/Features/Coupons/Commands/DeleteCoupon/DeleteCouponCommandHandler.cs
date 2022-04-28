using Application.Contracts.Persistence;
using MediatR;

namespace Application.Features.Coupons.Commands.DeleteCoupon
{
    public class DeleteCouponCommandHandler : IRequestHandler<DeleteCouponCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCouponCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var buyer = await _unitOfWork.Buyers.GetByIdAsync(request.BuyerId);
                if (buyer != null)
                {

                    var couponToDelete = await _unitOfWork.Coupons.GetByUniqueCodeAsync(request.Code, buyer.Id);
                    if(couponToDelete != null)
                    {
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
