using MediatR;
using Application.DtoModels.Order;
using Application.Contracts.Persistence;
using AutoMapper;

namespace Application.Features.Orders.Queries.GetOrderByBuyerId
{
    public class GetOrderByBuyerIdQueryHandler : IRequestHandler<GetOrderByBuyerIdQuery, OrderDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderByBuyerIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(GetOrderByBuyerIdQuery query, CancellationToken cancellationToken)
        {
            var orderByBuyer = await _unitOfWork.Orders.GetOrderByBuyerId(query.BuyerId);
            return _mapper.Map<OrderDto>(orderByBuyer);
        }
    }
}
