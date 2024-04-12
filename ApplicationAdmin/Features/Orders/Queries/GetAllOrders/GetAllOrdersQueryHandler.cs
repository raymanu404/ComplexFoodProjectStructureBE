using System.Linq.Expressions;
using System.Text.Json;
using ApplicationAdmin.Contracts.Abstractions;
using ApplicationAdmin.Contracts.Persistence;
using ApplicationAdmin.DtoModels.Order;
using ApplicationAdmin.DtoModels.Order;
using AutoMapper;
using Domain.Models.Ordering;
using Domain.Models.Shopping;
using HelperLibrary.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationAdmin.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, ResponseData<OrderDto>>
    {
        private readonly IUnitOfWorkAdmin _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllOrdersQueryHandler(IUnitOfWorkAdmin unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseData<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var req = request.SearchParams;
            Expression<Func<Order, bool>>? filters = null;
            Expression<Func<Order, dynamic>>? orderByKeySelector = null;

            var columnFilters = new List<ColumnFilter>();

            if (!string.IsNullOrEmpty(req.ColumnFilters))
            {
                try
                {
                    columnFilters.AddRange(JsonSerializer.Deserialize<List<ColumnFilter>>(req.ColumnFilters) ?? new List<ColumnFilter>());
                }
                catch (Exception)
                {
                    columnFilters = new List<ColumnFilter>();
                }
            }

            if (columnFilters.Count > 0)
            {
                filters = CustomExpressionFilter<Order>.CustomFilter(columnFilters, "Order");
            }

            if (!string.IsNullOrWhiteSpace(req.OrderBy))
            {
                orderByKeySelector = CustomExpressionFilter<Order>.CreateOrderByFunc<Order, dynamic>(req.OrderBy, "Order");
            }

            var ordersQuery = _unitOfWork.Orders.GetQueryable();
            var query = ordersQuery
                .CustomQuery(filters)
                .CustomOrderBy(orderByKeySelector, req.Asc);

            var count = query.Count();
            var filteredData = await query
                .CustomPagination(req.PageNumber, req.PageSize)
                .ToListAsync(cancellationToken);

            return new ResponseData<OrderDto>
            {
                Data = _mapper.Map<List<OrderDto>>(filteredData),
                TotalCount = count,
                CurrentPage = req.PageNumber
            };
        }
    }
}
