using MediatR;
using AutoMapper;
using Application.Contracts.Persistence;
using Domain.Models.Ordering;

namespace Application.Features.OrderItems.Commands.CreateItemCommand
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateItemCommand command, CancellationToken cancellationToken)
        {
            int id = 0;
            try
            {
                var getProductById = await _unitOfWork.Products.GetByIdAsync(command.OrderItem.ProductId);

                if(getProductById != null)
                {
                    var buyer = await _unitOfWork.Buyers.GetByIdAsync(command.BuyerId);

                    if(buyer != null)
                    {
                        var item = _mapper.Map<OrderItem>(command.OrderItem);
                      
                        //item.ProductId = getProductById.Id;
                        //item.Product = getProductById;
                        //await _unitOfWork.Items.CreateItemAsync(item);
                        await _unitOfWork.CommitAsync(cancellationToken);
                        id = item.OrderItemId;
                    }
                   
                }
                else
                {
                    return 0;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }

            return id;
        }
    }
}
