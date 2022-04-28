using MediatR;
using AutoMapper;
using Application.DtoModels.OrderItem;
using Application.Contracts.Persistence;
using Domain.Models.Ordering;

namespace Application.Features.OrderItems.Queries.GetItemById
{
    public class GetItemByIdQuyerHandler : IRequestHandler<GetItemByIdQuery, OrderItemDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetItemByIdQuyerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<OrderItemDto> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            //var itemById = await _unitOfWork.Items.GetItemByIdAsync(request.ItemId);          
            //return _mapper.Map<OrderItemDto>(itemById);
            throw new NotImplementedException();
        }
    }
}
