using ApplicationAdmin.Contracts.Abstractions;
using MediatR;
using ApplicationAdmin.Contracts.Persistence;
using AutoMapper;
using HelperLibrary.Constants;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Shopping;
using System.Linq.Expressions;
using static ApplicationAdmin.Features.Products.Queries.GetProductsByCalculus.Response;

namespace ApplicationAdmin.Features.Products.Queries.GetProductsByCalculus;
public class GetProductsByCalculusQueryHandler : IRequestHandler<GetProductsByCalculusQuery, Response>
{
    private readonly IUnitOfWorkAdmin _unitOfWork;
    public GetProductsByCalculusQueryHandler(IUnitOfWorkAdmin unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;

    }
    public async Task<Response> Handle(GetProductsByCalculusQuery request, CancellationToken cancellationToken)
    {

        const double withTva = 1 - Constants.TVA;

        var startDate = request.startDate;
        var endDate = request.endDate;

        int totalProducts = 0;
        int totalInStock = 0;
        int totalOutOfStock = 0;
        double totalMerchantPrice = 0;
        double totalPrice = 0;
        double totalProfitWithoutVTA = 0;
        double totalProfitWithVTA = 0;


        Expression<Func<Product, bool>>? predicate = null;

        if (endDate >= startDate)
        {
            predicate = p => p.DateCreated >= startDate && p.DateCreated <= endDate;
        }

        var products = await _unitOfWork.Products.GetQueryable()
            .Where(predicate)
            .ToListAsync(cancellationToken: cancellationToken);

        var productsGroupedByCategory = products
            .GroupBy(p => p.Category)
            .Select(g => new ResponseCalculus
            {
                CategoryName = g.Key.ToString(),
                TotalProducts = g.Count(),
                InStock = g.Count(p => p.IsInStock),
                OutOfStock = g.Count(p => !p.IsInStock),
                TotalPrice = g.Sum(p => p.Price.Value),
                TotalMerchantPrice = g.Sum(p => p.MerchantPrice.Value),
                TotalProfitWithoutVTA = g.Sum(p => p.Price.Value - p.MerchantPrice.Value),
                TotalProfitWithVTA = g.Sum(p => p.Price.Value - p.MerchantPrice.Value) * withTva
            })
            .ToList();


        foreach (var item in productsGroupedByCategory)
        {
            totalProducts += item.TotalProducts;
            totalInStock += item.InStock;
            totalOutOfStock += item.OutOfStock;
            totalProfitWithoutVTA += item.TotalProfitWithoutVTA;
            totalProfitWithVTA += item.TotalProfitWithVTA;
            totalMerchantPrice += item.TotalMerchantPrice;
            totalPrice += item.TotalPrice;
        }

        return new Response
        {
            CalculusData = new()
            {
                Data = productsGroupedByCategory,
                TotalCount = productsGroupedByCategory.Count
            },
            TotalProducts = totalProducts,
            TotalInStock = totalInStock,
            TotalProfitWithoutVTA = totalProfitWithoutVTA,
            TotalProfitWithVTA = totalProfitWithVTA,
            TotalOutOfStock = totalOutOfStock,
            TotalMerchantPrice = totalMerchantPrice,
            TotalPrice = totalPrice
        };

    }
}
