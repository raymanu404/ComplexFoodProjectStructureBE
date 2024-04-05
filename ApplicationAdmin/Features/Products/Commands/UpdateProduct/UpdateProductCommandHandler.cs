using ApplicationAdmin.Contracts.Persistence;
using ApplicationAdmin.DtoModels.Product;
using AutoMapper;
using Domain.ValueObjects;
using MediatR;

namespace ApplicationAdmin.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly IUnitOfWorkAdmin _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IUnitOfWorkAdmin unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productToUpdate = await _unitOfWork.Products.GetByIdAsync(request.ProductId);

            if (productToUpdate != null)
            {
                if (request.Product.Category == null)
                {
                    request.Product.Category = productToUpdate.Category;
                }

                if (request.Product.Title == null || request.Product.Title.Equals("string"))
                {
                    request.Product.Title = productToUpdate.Title;
                }

                if (request.Product.Description == null || request.Product.Description.Equals("string"))
                {
                    request.Product.Description = productToUpdate.Description;
                }

                if (request.Product.Price == null || request.Product.Price == 0.0f)
                {
                    request.Product.Price = productToUpdate.Price.Value;
                }

                if (request.Product.Image == null || request.Product.Image.Equals("string"))
                {
                    request.Product.Image = productToUpdate.Image;
                }

                if (request.Product.DateUpdated == null)
                {
                    request.Product.DateUpdated = productToUpdate.DateUpdated;
                }

                if (request.Product.IsInStock == null)
                {
                    request.Product.IsInStock = productToUpdate.IsInStock;
                }

                productToUpdate.Category = request.Product.Category;
                productToUpdate.Title = request.Product.Title;
                productToUpdate.Description = request.Product.Description;
                productToUpdate.Price = new Price(request.Product.Price);
                productToUpdate.Image = request.Product.Image;
                productToUpdate.DateCreated = request.Product.DateCreated;
                productToUpdate.DateUpdated = request.Product.DateUpdated;
                productToUpdate.IsInStock = request.Product.IsInStock;

                await _unitOfWork.CommitAsync(cancellationToken);
            }

            return _mapper.Map<ProductDto>(productToUpdate);
        }
    }
}
