using ApplicationAdmin.Contracts.Persistence;
using AutoMapper;
using HelperLibrary.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static ApplicationAdmin.Features.Orders.Queries.GetOrdersStatistics.Response;

namespace ApplicationAdmin.Features.Orders.Queries.GetOrdersStatistics;
public class GetOrdersStatisticsQueryHandler : IRequestHandler<GetOrdersStatisticsQuery, Response>
{
    private readonly IUnitOfWorkAdmin _unitOfWork;
    const double withTva = 1 - Constants.TVA;

    public GetOrdersStatisticsQueryHandler(IUnitOfWorkAdmin unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(GetOrdersStatisticsQuery request, CancellationToken cancellationToken)
    {

        var startDate = request.startDate;
        var endDate = request.endDate;

        var ordersByPeriod = await _unitOfWork.Orders.GetQueryable()
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Where(o => o.DatePlaced >= startDate && o.DatePlaced <= endDate)
            .ToListAsync(cancellationToken);

        var totalOrdersList = await _unitOfWork.Orders.GetQueryable()
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .ToListAsync(cancellationToken);

        var totalMerchantPriceByPeriod = ordersByPeriod
            .SelectMany(o => o.OrderItems)
            .Sum(oi => oi.Cantity.Value * oi.Product.MerchantPrice.Value);


        var totalDataMerchantPrice = totalOrdersList
                    .SelectMany(o => o.OrderItems)
                    .Sum(oi => oi.Cantity.Value * oi.Product.MerchantPrice.Value);


        var totalOrdersByPeriod = ordersByPeriod.Count;
        var totalPriceByPeriod = ordersByPeriod.Sum(o => o.TotalPrice.Value);
        var totalProfitWithoutVTAByPeriod = totalPriceByPeriod - totalMerchantPriceByPeriod;
        var totalProfitWithVTAByPeriod = totalProfitWithoutVTAByPeriod * withTva;


        var totalDataOrders = totalOrdersList.Count;
        var totalDataPrice = totalOrdersList.Sum(o => o.TotalPrice.Value);
        var totalDataProfitWithoutVTA = totalDataPrice - totalDataMerchantPrice;
        var totalDataProfitWithVTA = totalDataProfitWithoutVTA * withTva;


        var dataInPeriodOfTime = new DataInPeriodOfTime()
        {
            TotalOrders = totalOrdersByPeriod,
            TotalPrice = totalPriceByPeriod,
            TotalMerchantPrice = totalMerchantPriceByPeriod,
            TotalProfitWithoutVTA = totalProfitWithoutVTAByPeriod,
            TotalProfitWithVTA = totalProfitWithVTAByPeriod

        };

        var totalData = new TotalData()
        {
            TotalOrders = totalDataOrders,
            TotalPrice = totalDataPrice,
            TotalMerchantPrice = totalDataMerchantPrice,
            TotalProfitWithoutVTA = totalDataProfitWithoutVTA,
            TotalProfitWithVTA = totalDataProfitWithVTA,
        };

        var percentages = CalculatePercentages(dataInPeriodOfTime, totalData);

        return new Response()
        {
            DataInPeriodOfTimeResponse = dataInPeriodOfTime,
            TotalDataResponse = totalData,
            DataInPercentsResponse = percentages
        };
    }


    public static DataInPercents CalculatePercentages(DataInPeriodOfTime part, TotalData total)
    {
        var result = new DataInPercents
        {
            TotalOrders = CalculatePercentage(part.TotalOrders, total.TotalOrders),
            TotalPrice = CalculatePercentage(part.TotalPrice, total.TotalPrice),
            TotalMerchantPrice = CalculatePercentage(part.TotalMerchantPrice, total.TotalMerchantPrice),
            TotalProfitWithoutVTA = CalculatePercentage(part.TotalProfitWithoutVTA, total.TotalProfitWithoutVTA),
            TotalProfitWithVTA = CalculatePercentage(part.TotalProfitWithVTA, total.TotalProfitWithVTA)
        };

        return result;
    }

    private static double CalculatePercentage(double part, double total)
    {
        if (total == 0) return 0;
        return Math.Round((part / total) * 100, 2);
    }
}
