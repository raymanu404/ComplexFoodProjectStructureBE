using MediatR;

using AutoMapper;
using Application.Contracts.Persistence;
using Domain.Models.Shopping;
using Application.DtoModels.Product;

namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {

            try
            {
                var product = _mapper.Map<Product>(command.Product);
 
                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.CommitAsync(cancellationToken);
               
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            return command.Product;
        }
    }
}
