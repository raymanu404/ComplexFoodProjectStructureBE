using ApplicationAdmin.Contracts.Persistence;
using ApplicationAdmin.DtoModels.Product;
using AutoMapper;
using Domain.Models.Abstractions;
using HelperLibrary.Classes;
using MediatR;
using System.Linq.Expressions;
using System.Text.Json;
using Domain.Models.Shopping;
using Microsoft.EntityFrameworkCore;
using ApplicationAdmin.Contracts.Abstractions;

namespace ApplicationAdmin.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ResponseData<ProductDto>>
    {
        private readonly IUnitOfWorkAdmin _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllProductsQueryHandler(IUnitOfWorkAdmin unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<ResponseData<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var req = request.SearchParams;
            //TODO: add searchTerm + sorting
            Expression<Func<Product, bool>> filters = null;
            List<ColumnFilter> columnFilters = new List<ColumnFilter>();

            if (!string.IsNullOrEmpty(req.ColumnFilters))
            {
                try
                {
                    columnFilters.AddRange(JsonSerializer.Deserialize<List<ColumnFilter>>(req.ColumnFilters));
                }
                catch (Exception)
                {
                    columnFilters = new List<ColumnFilter>();
                }
            }

            if (columnFilters.Count > 0)
            {
                filters = CustomExpressionFilter<Product>.CustomFilter(columnFilters, "Product");
            }

            var productsQuery = _unitOfWork.Products.GetQueryable();
            var query = productsQuery.CustomQuery(filters);
            var count = query.Count();
            var filteredData = await query.CustomPagination(req.PageNumber, req.PageSize).ToListAsync(cancellationToken);

            return new ResponseData<ProductDto>
            {
                Data = _mapper.Map<List<ProductDto>>(filteredData),
                TotalCount = count,
                CurrentPage = req.PageNumber
            };
        }
    }
}

