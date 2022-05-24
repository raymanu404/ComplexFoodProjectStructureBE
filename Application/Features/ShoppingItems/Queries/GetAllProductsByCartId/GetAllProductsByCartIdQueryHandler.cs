using MediatR;
using Application.DtoModels.Product;
using Application.Contracts.Persistence;
using Application.Features.Products.Queries.GetProductById;
using AutoMapper;

namespace Application.Features.ShoppingItems.Queries.GetAllProductsByCartId
{
    public class GetAllProductsByCartIdQueryHandler : IRequestHandler<GetAllProductsByCartIdQuery, List<ProductFromCartDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetAllProductsByCartIdQueryHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<List<ProductFromCartDto>> Handle(GetAllProductsByCartIdQuery request, CancellationToken cancellationToken)
        {
            List<ProductFromCartDto> products = new List<ProductFromCartDto>();
            var shoppingItemsByCartId = await _unitOfWork.ShoppingItems.GetAllShoppingItemsByShoppingCartId(request.ShoppingCartId);
            if(shoppingItemsByCartId.Count != 0)
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

            return products;
        }
    }
}
