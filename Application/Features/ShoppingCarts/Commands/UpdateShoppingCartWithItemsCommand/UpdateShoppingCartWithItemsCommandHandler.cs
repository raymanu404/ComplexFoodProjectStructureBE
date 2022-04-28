using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.ShoppingCarts.Commands.AddShoppingCartItemsCommand
{
    public class UpdateShoppingCartWithItemsCommandHandler : IRequestHandler<UpdateShoppingCartWithItemsCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateShoppingCartWithItemsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateShoppingCartWithItemsCommand command, CancellationToken cancellationToken)
        {

            //momementan doar adaugam pur si simplu in lista de item-uri dupa mai vedem daca facem aici verificarile
            var getCartByBuyerId = await _unitOfWork.ShoppingCarts.GetCartByBuyerIdAsync(command.BuyerId);
            if(getCartByBuyerId != null)
            {
                //var item =
                //getCartByBuyerId.Items.Add();
                await _unitOfWork.CommitAsync(cancellationToken);
            }
           
            return Unit.Value;
        }
    }
}
