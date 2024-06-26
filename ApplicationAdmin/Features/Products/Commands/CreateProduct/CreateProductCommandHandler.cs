﻿using ApplicationAdmin._Utils;
using ApplicationAdmin.Contracts.Persistence;
using AutoMapper;
using Domain.Models.Shopping;
using MediatR;

namespace ApplicationAdmin.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAdmin _unitOfWork;

        public CreateProductCommandHandler(IUnitOfWorkAdmin unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<string> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var returnMessage = "";
            try
            {
                if (HelpersFn.IsInRangeCategories((int)command.Product.Category))
                {
                    var product = _mapper.Map<Product>(command.Product);
                    await _unitOfWork.Products.AddAsync(product);
                    await _unitOfWork.CommitAsync(cancellationToken);
                    returnMessage = "Product was created successfully!";
                }
                else
                {
                    returnMessage = "Out of range in Categories";
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                returnMessage = "Error in Create Product";
            }
            return returnMessage;
        }

       
    }

}
