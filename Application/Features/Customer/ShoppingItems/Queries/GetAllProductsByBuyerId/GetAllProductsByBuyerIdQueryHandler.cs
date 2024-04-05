using MediatR;
using Application.DtoModels.Product;
using AutoMapper;
using Application.Contracts.Persistence.Customer;
using Application.Features.Customer.Products.Queries.GetProductById;

namespace Application.Features.Customer.ShoppingItems.Queries.GetAllProductsByBuyerId
{
    public class GetAllProductsByBuyerIdQueryHandler : IRequestHandler<GetAllProductsByBuyerIdQuery, List<ProductFromCartDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetAllProductsByBuyerIdQueryHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<List<ProductFromCartDto>> Handle(GetAllProductsByBuyerIdQuery request, CancellationToken cancellationToken)
        {
            List<ProductFromCartDto> products = new List<ProductFromCartDto>();
            var getCartByBuyerId = await _unitOfWork.ShoppingCarts.GetCartByBuyerIdAsync(request.BuyerId);
            if (getCartByBuyerId != null)
            {
                var shoppingItemsByCartId = await _unitOfWork.ShoppingItems.GetAllShoppingItemsByShoppingCartId(getCartByBuyerId.Id);
                if (shoppingItemsByCartId.Count != 0)
                {
                    foreach (var shoppingItem in shoppingItemsByCartId)
                    {
                        var commandGetProduct = new GetProductByIdQuery
                        {
                            ProductId = shoppingItem.ProductId
                        };

                        var productsByCartId = await _mediator.Send(commandGetProduct);
                        var productFromCartDto = _mapper.Map<ProductFromCartDto>(productsByCartId);
                        productFromCartDto.Cantity = shoppingItem.Cantity.Value;
                        products.Add(productFromCartDto);
                    }
                }
            }


            return products;
        }
    }
}
