using MediatR;

namespace ApplicationAdmin.Features.Orders.Queries.GetOrdersStatistics;
public class GetOrdersStatisticsQuery : IRequest<Response>
{
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
}
