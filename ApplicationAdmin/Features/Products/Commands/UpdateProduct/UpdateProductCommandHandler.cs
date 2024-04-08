using ApplicationAdmin._Utils;
using ApplicationAdmin.Contracts.Persistence;
using ApplicationAdmin.DtoModels.Product;
using ApplicationAdmin.Profiles;
using AutoMapper;
using Domain.Models.Enums;
using Domain.ValueObjects;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ApplicationAdmin.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, StatusCodeEnum>
    {
        private readonly IUnitOfWorkAdmin _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWorkAdmin unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<StatusCodeEnum> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productToUpdate = await _unitOfWork.Products.GetByIdAsync(request.ProductId);

            if (productToUpdate == null)
            {
                return StatusCodeEnum.NotFound;
            }

            if (!string.IsNullOrWhiteSpace(request.Product.Title))
            {
                productToUpdate.Title = request.Product.Title;
            }

            if (!string.IsNullOrWhiteSpace(request.Product.Description))
            {
                productToUpdate.Description = request.Product.Description;
            }

            if (!string.IsNullOrWhiteSpace(request.Product.Image))
            {
                productToUpdate.Image = request.Product.Image;
            }

            if (request.Product.Price > 0 && request.Product.Price != null)
            {
                productToUpdate.Price = new Price(request.Product.Price ?? 0);
            }
            if (!string.IsNullOrWhiteSpace(request.Product.Image))
            {
                productToUpdate.Image = request.Product.Image;
            }

            if (request.Product.IsInStock != null)
            {
                productToUpdate.IsInStock = request.Product.IsInStock ?? false;
            }

            if (HelpersFn.IsInRangeCategories((int)request.Product.Category))
            {
                productToUpdate.Category = request.Product.Category ?? Categories.Soup;
            }

            productToUpdate.DateUpdated = DateTime.Now;
          
            await _unitOfWork.CommitAsync(cancellationToken);

            return StatusCodeEnum.Success;
        }
    }
}