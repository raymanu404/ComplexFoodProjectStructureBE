using Application.Contracts.Persistence.Admin;
using MediatR;
using Application.DtoModels.Product;
using AutoMapper;


namespace Application.Features.Admin.Products.Queries.GetProductById
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
