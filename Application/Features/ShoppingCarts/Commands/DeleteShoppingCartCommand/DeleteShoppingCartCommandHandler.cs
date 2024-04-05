using MediatR;
using Application.Contracts.Persistence;

namespace Application.Features.ShoppingCarts.Commands.DeleteShoppingCartCommand
{
    public class DeleteShoppingCartCommandHandler : IRequestHandler<DeleteShoppingCartCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteShoppingCartCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(DeleteShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var returnMessage = "";
            try
            {
                var cart = await _unitOfWork.ShoppingCarts.GetCartByBuyerIdAsync(request.BuyerId);
                if (cart != null)
                {
                    _unitOfWork.ShoppingCarts.Delete(cart);
                    await _unitOfWork.CommitAsync(cancellationToken);
                    returnMessage = "Success!";
                }
                else
                {
                    returnMessage = "Cart doesn't exists!";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Exception on Delete";
            }

            return returnMessage;
        }
    }
}
