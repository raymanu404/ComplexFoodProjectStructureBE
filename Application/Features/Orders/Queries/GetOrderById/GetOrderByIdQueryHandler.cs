using MediatR;
using Application.DtoModels.Order;
using Application.Contracts.Persistence;
using AutoMapper;

namespace Application.Features.Orders.Queries.GetOrderByBuyerId
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            var orderByBuyer = await _unitOfWork.Orders.GetOrderByBuyerId(query.OrderId);
            return _mapper.Map<OrderDto>(orderByBuyer);
        }
    }
}
