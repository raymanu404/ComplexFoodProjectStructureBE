using MediatR;
using Application.Contracts.Persistence;
using Application.DtoModels.Product;
using AutoMapper;

namespace Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var productById = await _unitOfWork.Products.GetByIdAsync(request.ProductId);          
            return _mapper.Map<ProductDto>(productById);
        }
    }
}
