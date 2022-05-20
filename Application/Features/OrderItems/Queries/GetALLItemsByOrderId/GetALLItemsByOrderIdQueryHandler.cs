using MediatR;
using Application.DtoModels.OrderItem;
using Application.Contracts.Persistence;
using AutoMapper;

namespace Application.Features.OrderItems.Queries.GetALLItemsByOrderId
{
    public class GetALLItemsByOrderIdQueryHandler : IRequestHandler<GetALLItemsByOrderIdQuery, List<OrderItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetALLItemsByOrderIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<OrderItemDto>> Handle(GetALLItemsByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var getAllItemsByOrderId = await _unitOfWork.OrderItems.GetAllItemsByOrderId(request.OrderId);
            return _mapper.Map<List<OrderItemDto>>(getAllItemsByOrderId);
        }
    }
}
