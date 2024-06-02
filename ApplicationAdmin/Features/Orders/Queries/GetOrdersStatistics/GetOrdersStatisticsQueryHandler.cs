using ApplicationAdmin.Contracts.Persistence;
using AutoMapper;
using HelperLibrary.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationAdmin.Features.Orders.Queries.GetOrdersStatistics;
public class GetOrdersStatisticsQueryHandler : IRequestHandler<GetOrdersStatisticsQuery, Response>
{
    private readonly IUnitOfWorkAdmin _unitOfWork;
    private readonly IMapper _mapper;
    const double withTva = 1 - Constants.TVA;

    public GetOrdersStatisticsQueryHandler(IUnitOfWorkAdmin unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Response> Handle(GetOrdersStatisticsQuery request, CancellationToken cancellationToken)
    {

        var startDate = request.startDate;
        var endDate = request.endDate;

        var orders = await _unitOfWork.Orders.GetQueryable()
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Where(o => o.DatePlaced >= startDate && o.DatePlaced <= endDate)
            .ToListAsync(cancellationToken);

        var totalMerchantPrice = orders
            .SelectMany(o => o.OrderItems)
            .Sum(oi => oi.Cantity.Value * oi.Product.MerchantPrice.Value);

        var totalOrders = orders.Count;
        var totalPrice = orders.Sum(o => o.TotalPrice.Value);
        var totalProfitWithoutVTA = totalPrice - totalMerchantPrice;
        var totalProfitWithVTA = totalProfitWithoutVTA * withTva;

        return new Response()
        {
            TotalOrders = totalOrders,
            TotalPrice = totalPrice,
            TotalProfitWithVTA = totalProfitWithVTA,
            TotalProfitWithoutVTA = totalProfitWithoutVTA,
            TotalMerchantPrice = totalMerchantPrice
        };
    }
}
