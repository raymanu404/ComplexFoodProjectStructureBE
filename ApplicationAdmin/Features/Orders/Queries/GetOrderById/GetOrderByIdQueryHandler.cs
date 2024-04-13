using ApplicationAdmin.Contracts.Persistence;
using ApplicationAdmin.DtoModels.Order;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationAdmin.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAdmin _unitOfWork;

        public GetOrderByIdQueryHandler(IUnitOfWorkAdmin unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            var orderByBuyerQuery = _unitOfWork.Orders
                .GetOrderByIdQuery(query.OrderId)
                .Include(item => item.OrderItems);

            var order = await orderByBuyerQuery.FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<OrderDto>(order);
        }
    }
}
