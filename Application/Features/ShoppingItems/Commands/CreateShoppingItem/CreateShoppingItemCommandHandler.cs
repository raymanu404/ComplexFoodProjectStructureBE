using MediatR;
using Application.Contracts.Persistence;
using Application.DtoModels.Cart;
using Domain.ValueObjects;
using Application.Features.ShoppingCarts.Commands.CreateShoppingCartCommand;
using Application.Components.RandomCode;
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

        public async Task<int> Handle(CreateShoppingItemCommand command, CancellationToken cancellationToken)
        {
            var id = 0;

            try
            {
                var buyer = await _unitOfWork.Buyers.GetByIdAsync(command.BuyerId);
                var product = await _unitOfWork.Products.GetByIdAsync(command.ProductId);

                if(buyer != null && product != null)
                {
                    var total_price = (command.Cantity * product.Price.Value);
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
                                Code = RandomCode.GetRandomCode(6),
                                DatePlaced = DateTime.Now,
                                Discount = 0,
                                TotalPrice = total_price
                            };

                            var cart = await _mediator.Send(new CreateShoppingCartCommand { BuyerId = buyer.Id, Cart = newCart });
                            shoppingCartId = cart.Id;

                        }
                        else
                        {
                            shoppingCartId = getCart.Id;
                            //getCart.TotalPrice = new Price(getCart.TotalPrice.Value + total_price);
                        }
                      
                        var shoppingItemDto = new ShoppingCartItemDto
                        {
                            ShoppingCartId = shoppingCartId,
                            ProductId = product.Id,                         
                            Cantity = command.Cantity

                        };
                        //verificat productId daca exista facem update cantitate, daca nu adaugam normal

                        var getItem = await _unitOfWork.ShoppingItems.GetShoppingItemByIds(shoppingItemDto.ShoppingCartId, shoppingItemDto.ProductId);
                        if(getItem != null) //update
                        {
                            if (buyer.Balance.Value >= getCart.TotalPrice.Value)
                            {
                                var updateCommand = new UpdateCantityShoppingItemCommand
                                {
                                    ShoppingCartId = shoppingItemDto.ShoppingCartId,
                                    ProductId = shoppingItemDto.ProductId,
                                    Cantity = shoppingItemDto.Cantity,
                                    BuyerId = command.BuyerId
                                };

                                var message = await _mediator.Send(updateCommand);
                                id = updateCommand.ShoppingCartId;

                            }
                            else
                            {
                                id = -2;
                            }

                        }//create 
                        else
                        {
                            getCart.TotalPrice = new Price(getCart.TotalPrice.Value + total_price);
                            var shoppingItem = _mapper.Map<ShoppingCartItem>(shoppingItemDto);
                            await _unitOfWork.ShoppingItems.AddAsync(shoppingItem);
                            buyer.Balance = new Balance(buyer.Balance.Value - total_price);
                            id = shoppingItem.ShoppingCartId;
                        }
                                       
                        await _unitOfWork.CommitAsync(cancellationToken);
                        
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

      
    }
}
