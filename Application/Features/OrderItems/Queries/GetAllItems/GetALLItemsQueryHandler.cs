using MediatR;
using Application.DtoModels.OrderItem;
using Application.Contracts.Persistence;
using AutoMapper;

namespace Application.Features.OrderItems.Queries.GetAllItems
{
    public class GetALLItemsQueryHandler : IRequestHandler<GetALLItemsQuery, List<OrderItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetALLItemsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<OrderItemDto>> Handle(GetALLItemsQuery request, CancellationToken cancellationToken)
        {
            var getAllItems = await _unitOfWork.OrderItems.GetAllItems();
            return _mapper.Map<List<OrderItemDto>>(getAllItems);
        }
    }
}
