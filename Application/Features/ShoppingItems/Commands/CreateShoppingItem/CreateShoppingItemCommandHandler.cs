using Application.Components;
using MediatR;
using Domain.ValueObjects;
using AutoMapper;
using Application.DtoModels.ShoppingCart;
using Application.DtoModels.ShoppingCartItem;
using Application.Features.ShoppingCarts.Commands.CreateShoppingCartCommand;
using Application.Features.ShoppingItems.Commands.UpdateShoppingItem;
using Application.Contracts.Persistence;
using Domain.Models.Shopping;

namespace Application.Features.ShoppingItems.Commands.CreateShoppingItem
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

                if (buyer != null && product != null)
                {
                    var total_price = command.Cantity * product.Price.Value;
                    if (buyer.Balance.Value >= total_price)
                    {
                        var shoppingCartId = 0;
                        var getCart = await _unitOfWork.ShoppingCarts.GetCartByBuyerIdAsync(buyer.Id);
                        var totalPriceFromCart = 0.0;
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
                            totalPriceFromCart = cart.TotalPrice.Value;

                        }
                        else
                        {
                            shoppingCartId = getCart.Id;
                            totalPriceFromCart = getCart.TotalPrice.Value;
                            //getCart.TotalPrice = new Price(totalPriceFromCart + total_price);
                        }

                        var shoppingItemDto = new ShoppingCartItemDto
                        {

                            ProductId = product.Id,
                            Cantity = command.Cantity

                        };

                        //verificat productId daca exista facem update cantitate, daca nu adaugam normal
                        var getItem = await _unitOfWork.ShoppingItems.GetShoppingItemByIds(shoppingCartId, shoppingItemDto.ProductId);
                        if (getItem != null) //update
                        {
                            if (buyer.Balance.Value >= totalPriceFromCart)
                            {
                                var updateCommand = new UpdateCantityShoppingItemCommand
                                {
                                    ShoppingCartId = shoppingCartId,
                                    ProductId = shoppingItemDto.ProductId,
                                    Cantity = shoppingItemDto.Cantity,
                                    BuyerId = command.BuyerId
                                };

                                var responseId = await _mediator.Send(updateCommand);
                                if (responseId > 0)
                                {
                                    id = updateCommand.ShoppingCartId;
                                }
                                else
                                {
                                    id = responseId;
                                }


                            }
                            else
                            {
                                id = -2;
                            }

                        }//create 
                        else
                        {
                            if (getCart != null)
                            {
                                getCart.TotalPrice = new Price(totalPriceFromCart + total_price);
                            }

                            var shoppingItem = _mapper.Map<ShoppingCartItem>(shoppingItemDto);
                            shoppingItem.ShoppingCartId = shoppingCartId;
                            await _unitOfWork.ShoppingItems.AddAsync(shoppingItem);

                            //buyer.Balance = new Balance(buyer.Balance.Value - total_price);
                            //nu are rost sa scadem de aici ca oricum scadem cu tot cu discount din orders
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
                return -1;
            }

            return id;
        }


    }
}
