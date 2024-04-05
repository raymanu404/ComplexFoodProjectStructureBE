using MediatR;
using AutoMapper;
using Application.DtoModels.ShoppingCart;
using Application.Contracts.Persistence;

namespace Application.Features.ShoppingCarts.Queries.GetShoppingCartByBuyerIdQuery
{
    public class GetShoppingCartByBuyerQueryHandler : IRequestHandler<GetShoppingCartByBuyerQuery, ShoppingCartDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetShoppingCartByBuyerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ShoppingCartDto> Handle(GetShoppingCartByBuyerQuery request, CancellationToken cancellationToken)
        {

            var cart = await _unitOfWork.ShoppingCarts.GetCartByBuyerIdAsync(request.BuyerId);
            return _mapper.Map<ShoppingCartDto>(cart);
        }
    }
}
