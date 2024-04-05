using MediatR;
using Application.DtoModels.OrderItem;
using AutoMapper;
using Application.Contracts.Persistence.Customer;

namespace Application.Features.Customer.OrderItems.Queries.GetAllItems
{
    public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, List<OrderItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllItemsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<OrderItemDto>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        {
            var getAllItems = await _unitOfWork.OrderItems.GetAllItems();
            return _mapper.Map<List<OrderItemDto>>(getAllItems);
        }
    }
}
