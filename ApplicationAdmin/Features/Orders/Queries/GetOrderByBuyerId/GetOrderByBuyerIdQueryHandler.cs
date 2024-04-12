using ApplicationAdmin.Contracts.Persistence;
using ApplicationAdmin.DtoModels.Order;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationAdmin.Features.Orders.Queries.GetOrderByBuyerId
{
    public class GetOrderByBuyerIdQueryHandler : IRequestHandler<GetOrderByBuyerIdQuery, OrderDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAdmin _unitOfWork;

        public GetOrderByBuyerIdQueryHandler(IUnitOfWorkAdmin unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(GetOrderByBuyerIdQuery query, CancellationToken cancellationToken)
        {
            var orderByBuyerQuery =  _unitOfWork.Orders.GetOrderByBuyerIdQuery(query.BuyerId);
            var order = await orderByBuyerQuery.FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<OrderDto>(order);
        }
    }
}
