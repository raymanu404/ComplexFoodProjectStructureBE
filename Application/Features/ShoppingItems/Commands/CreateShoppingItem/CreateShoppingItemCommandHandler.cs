using MediatR;
using Application.Contracts.Persistence;
using Application.DtoModels.Cart;
using Domain.ValueObjects;
using Application.Features.ShoppingCarts.Commands.CreateShoppingCartCommand;
using Domain.Models.Shopping;
using Application.DtoModels.ShoppingCartItemDto;
using AutoMapper;

namespace Application.Features.ShoppingItems.Commands
{
    public class CreateShoppingItemCommandHandler : IRequestHandler<CreateShoppingItemCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateShoppingItemCommandHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateShoppingItemCommand request, CancellationToken cancellationToken)
        {
            var id = 0;

            try
            {
                var buyer = await _unitOfWork.Buyers.GetByIdAsync(request.BuyerId);
                var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);

                if(buyer != null && product != null)
                {
                    var total_price = (request.Amount * product.Price.Value);
                    if(buyer.Balance.Value >= total_price)
                    {
                        var shoppingCartId = 0;
                        var getCart = await _unitOfWork.ShoppingCarts.GetCartByBuyerIdAsync(buyer.Id);
                        if (getCart == null)
                        {
                            //facem cart inainte
                            var newCart = new ShoppingCartDto
                            {
                                BuyerId = buyer.Id,
                                Code = RandomCode(6),
                                DatePlaced = DateTime.Now,
                                Discount = 0,
                                TotalPrice = total_price
                            };
                            var cart = await _mediator.Send(new CreateShoppingCartCommand { BuyerId = buyer.Id, Cart = newCart });
                            await _unitOfWork.CommitAsync(cancellationToken);
                            shoppingCartId = cart.Id;

                        }
                        else
                        {
                            shoppingCartId = getCart.Id;

                        }

                        var shoppingItemDto = new ShoppingCartItemDto
                        {

                            ProductId = product.Id,
                            ShoppingCartId = shoppingCartId,
                            Amount = request.Amount

                        };

                        var shoppingItem = _mapper.Map<ShoppingCartItem>(shoppingItemDto);
                        await _unitOfWork.ShoppingItems.AddAsync(shoppingItem);
                        buyer.Balance = new Balance(buyer.Balance.Value - total_price);

                        await _unitOfWork.CommitAsync(cancellationToken);
                        id = shoppingItem.ShoppingCartId;
                    }
                    else
                    {
                        id = -2;
                    }
                
                }
                else
                {
                    id = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return  -1;
            }

            return id;
        }

        private static string RandomCode(int length)
        {
            var ran = new Random();

            var b = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            string random = "";

            for (var i = 0; i < length; i++)
            {
                var a = ran.Next(b.Length);
                random = random + b.ElementAt(a);
            }

            return random;
        }
    }
}
