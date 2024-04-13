using ApplicationAdmin.Contracts.Persistence;
using ApplicationAdmin.DtoModels.OrderItem;
using AutoMapper;
using MediatR;

namespace ApplicationAdmin.Features.OrderItems.Queries.GetAllOrderItemsByOrderId
{
    public class GetAllOrderItemsByOrderIdQueryHandler : IRequestHandler<GetAllOrderItemsByOrderIdQuery, IList<OrderItemDto>>
    {
        private readonly IUnitOfWorkAdmin _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllOrderItemsByOrderIdQueryHandler(IUnitOfWorkAdmin unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IList<OrderItemDto>> Handle(GetAllOrderItemsByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var getAllItemsByOrderId = await _unitOfWork.OrderItems.GetAllItemsByOrderId(request.OrderId);
            return _mapper.Map<List<OrderItemDto>>(getAllItemsByOrderId);
        }
    }
}
