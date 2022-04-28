using MediatR;
using Application.Contracts.Persistence;
using Application.DtoModels.Product;
using AutoMapper;

namespace Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

         }
        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
