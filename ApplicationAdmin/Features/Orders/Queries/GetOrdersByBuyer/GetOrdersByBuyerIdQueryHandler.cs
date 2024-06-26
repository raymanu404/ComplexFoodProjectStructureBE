﻿using ApplicationAdmin.Contracts.Abstractions;
using ApplicationAdmin.Contracts.Persistence;
using ApplicationAdmin.DtoModels.Order;
using AutoMapper;
using Domain.Models.Ordering;
using HelperLibrary.Classes;
using MediatR;
using System.Linq.Expressions;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace ApplicationAdmin.Features.Orders.Queries.GetOrdersByBuyer
{
    public class GetOrdersByBuyerIdQueryHandler : IRequestHandler<GetOrdersByBuyerIdQuery, ResponseData<OrderDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAdmin _unitOfWork;

        public GetOrdersByBuyerIdQueryHandler(IUnitOfWorkAdmin unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseData<OrderDto>> Handle(GetOrdersByBuyerIdQuery request, CancellationToken cancellationToken)
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

            var orderByBuyerQuery = _unitOfWork.Orders.GetOrderByBuyerIdQuery(request.BuyerId);
            
            var query = orderByBuyerQuery
                .CustomQuery(filters)
                .Include(item => item.OrderItems)
                .Include(item => item.Buyer)
                .CustomOrderBy(orderByKeySelector, req.Asc);

            var count = query.Count();
            var filteredData = await query
                .CustomPagination(req.PageNumber, req.PageSize)
                .ToListAsync(cancellationToken);

            return new ResponseData<OrderDto>
            {
                Data = _mapper.Map<List<OrderDto>>(filteredData),
                TotalCount = count,
            };
        }
    }
}
