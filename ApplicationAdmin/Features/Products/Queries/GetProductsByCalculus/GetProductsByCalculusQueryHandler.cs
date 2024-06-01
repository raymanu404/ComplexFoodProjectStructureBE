using ApplicationAdmin.Contracts.Abstractions;
using MediatR;
using ApplicationAdmin.Contracts.Persistence;
using AutoMapper;
using HelperLibrary.Constants;
using Microsoft.EntityFrameworkCore;

namespace ApplicationAdmin.Features.Products.Queries.GetProductsByCalculus;
public class GetProductsByCalculusQueryHandler : IRequestHandler<GetProductsByCalculusQuery, ResponseData<ResponseProduct>>
{
    private readonly IUnitOfWorkAdmin _unitOfWork;
    private readonly IMapper _mapper;
    public GetProductsByCalculusQueryHandler(IUnitOfWorkAdmin unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;

    }
    public async Task<ResponseData<ResponseProduct>> Handle(GetProductsByCalculusQuery request, CancellationToken cancellationToken)
    {

        const double withoutTva = 1 - Constants.TVA;
        const double withTva = 1 + Constants.TVA;

        var products = await _unitOfWork.Products.GetQueryable()
            //.Where(p => p.DateCreated >= startDate && p.DateCreated <= endDate)
            .ToListAsync(cancellationToken: cancellationToken);

        var productsGroupedByCategory = products
            .GroupBy(p => p.Category)
            .Select(g => new ResponseProduct
            {
                CategoryName = g.Key.ToString(),
                TotalProducts = g.Count(),
                InStock = g.Count(p => p.IsInStock),
                OutOfStock = g.Count(p => !p.IsInStock),
                TotalPrice = g.Sum(p => p.Price.Value),
                TotalSellingPrice = g.Sum(p => p.SellingPrice.Value),
                TotalProfit = g.Sum(p =>  p.Price.Value - p.SellingPrice.Value),
                TotalProfitWithoutVTA = g.Sum(p => p.Price.Value - p.SellingPrice.Value) * withoutTva, // Assuming 20% VTA
                TotalProfitWithVTA = g.Sum(p => p.Price.Value - p.SellingPrice.Value) * withTva  // Assuming 20% VTA
            })
            .ToList();


        return new ResponseData<ResponseProduct>
        {
            Data = productsGroupedByCategory,
            TotalCount = productsGroupedByCategory.Count,
        };

    }
}
