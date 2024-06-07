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

        var orders = await _unitOfWork.Orders.GetQueryable()
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Where(o => o.DatePlaced >= startDate && o.DatePlaced <= endDate)
            .ToListAsync(cancellationToken);

        var totalOrdersList = await _unitOfWork.Orders.GetQueryable()
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .ToListAsync(cancellationToken);

        var totalMerchantPrice = orders
            .SelectMany(o => o.OrderItems)
            .Sum(oi => oi.Cantity.Value * oi.Product.MerchantPrice.Value);


        var totalDataMerchantPrice = totalOrdersList
                    .SelectMany(o => o.OrderItems)
                    .Sum(oi => oi.Cantity.Value * oi.Product.MerchantPrice.Value);


        var totalOrders = orders.Count;
        var totalPrice = orders.Sum(o => o.TotalPrice.Value);
        var totalProfitWithoutVTA = totalPrice - totalMerchantPrice;
        var totalProfitWithVTA = totalProfitWithoutVTA * withTva;


        var totalDataOrders = totalOrdersList.Count;
        var totalDataPrice = totalOrdersList.Sum(o => o.TotalPrice.Value);
        var totalDataProfitWithoutVTA = totalDataPrice - totalDataMerchantPrice;
        var totalDataProfitWithVTA = totalDataProfitWithoutVTA * withTva;


        var dataInPeriodOfTime = new DataInPeriodOfTime()
        {
            TotalOrders = totalOrders,
            TotalPrice = totalPrice,
            TotalProfitWithVTA = totalProfitWithVTA,
            TotalProfitWithoutVTA = totalProfitWithoutVTA,
            TotalMerchantPrice = totalMerchantPrice
        };

        var totalData = new TotalData()
        {
            TotalOrders = totalDataOrders,
            TotalPrice = totalDataPrice,
            TotalProfitWithVTA = totalDataProfitWithVTA,
            TotalProfitWithoutVTA = totalDataProfitWithoutVTA,
            TotalMerchantPrice = totalDataProfitWithVTA
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
