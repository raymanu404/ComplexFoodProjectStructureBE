using MediatR;
using Application.DtoModels.OrderItem;
using AutoMapper;
using Application.Contracts.Persistence;

namespace Application.Features.OrderItems.Queries.GetALLItemsByOrderId
{
    public class GetAllItemsByOrderIdQueryHandler : IRequestHandler<GetAllItemsByOrderIdQuery, List<OrderItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllItemsByOrderIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<OrderItemDto>> Handle(GetAllItemsByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var getAllItemsByOrderId = await _unitOfWork.OrderItems.GetAllItemsByOrderId(request.OrderId);
            return _mapper.Map<List<OrderItemDto>>(getAllItemsByOrderId);
        }
    }
}
