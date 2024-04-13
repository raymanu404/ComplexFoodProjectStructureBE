using ApplicationAdmin.Contracts.Persistence;
using ApplicationAdmin.DtoModels.OrderItem;
using AutoMapper;
using MediatR;

namespace ApplicationAdmin.Features.OrderItems.Queries.GetAllItems
{
    public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, List<OrderItemDto>>
    {
        private readonly IUnitOfWorkAdmin _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllItemsQueryHandler(IUnitOfWorkAdmin unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //TODO: for now we don't know if do we need this, but for future maybe we can get all items that was ordered most
        //TODO: and then we can compute how much and what resources/recipes/ingredients we should spend on one month for instance
        public async Task<List<OrderItemDto>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        {
            var getAllItems = await _unitOfWork.OrderItems.GetAllItems();
            return _mapper.Map<List<OrderItemDto>>(getAllItems);
        }
    }
}
