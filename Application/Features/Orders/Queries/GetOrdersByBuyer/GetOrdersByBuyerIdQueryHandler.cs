using MediatR;
using Application.DtoModels.Order;
using AutoMapper;
using Application.Contracts.Persistence;

namespace Application.Features.Orders.Queries.GetOrdersByBuyer
{
    public class GetOrdersByBuyerIdQueryyHandler : IRequestHandler<GetOrdersByBuyerIdQuery, List<OrderDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetOrdersByBuyerIdQueryyHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> Handle(GetOrdersByBuyerIdQuery query, CancellationToken cancellationToken)
        {
            var ordersByBuyer = await _unitOfWork.Orders.GetAllOrdersByBuyerId(query.BuyerId);
            return _mapper.Map<List<OrderDto>>(ordersByBuyer);
        }
    }
}
