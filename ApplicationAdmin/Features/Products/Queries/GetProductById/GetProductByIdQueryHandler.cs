using ApplicationAdmin.Contracts.Persistence;
using ApplicationAdmin.DtoModels.Product;
using AutoMapper;
using MediatR;

namespace ApplicationAdmin.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAdmin _unitOfWork;

        public GetProductByIdQueryHandler(IUnitOfWorkAdmin unitOfWork, IMapper mapper)
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
