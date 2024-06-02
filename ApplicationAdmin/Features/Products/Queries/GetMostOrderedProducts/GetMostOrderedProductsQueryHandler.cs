using ApplicationAdmin.Contracts.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace ApplicationAdmin.Features.Products.Queries.GetMostOrderedProducts;
public class GetMostOrderedProductsQueryHandler : IRequestHandler<GetMostOrderedProductsQuery, Response>
{
    private const int MAX_MOST_ORDERED_PRODUCTS_COUNT = 10;
    private readonly IUnitOfWorkAdmin _unitOfWork;
    private readonly IMapper _mapper;

    public GetMostOrderedProductsQueryHandler(IUnitOfWorkAdmin unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Response> Handle(GetMostOrderedProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.GetQueryable()
            .Where(x => x.MostOrderedProductCount > 0)
            .OrderByDescending(x => x.MostOrderedProductCount)
            .Take(MAX_MOST_ORDERED_PRODUCTS_COUNT)
            .ToListAsync(cancellationToken);

        return new Response
        {
            Data = new()
            {
                TotalCount = products.Count,
                Data = _mapper.Map<List<Response.ProductDto>>(products)
            }
        };
    }
}
