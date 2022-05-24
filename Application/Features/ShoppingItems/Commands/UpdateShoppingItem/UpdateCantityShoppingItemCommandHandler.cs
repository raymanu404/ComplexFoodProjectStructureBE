using MediatR;
using Application.Contracts.Persistence;
using Domain.ValueObjects;

namespace Application.Features.ShoppingItems.Commands
{
    public class UpdateCantityShoppingItemCommandHandler : IRequestHandler<UpdateCantityShoppingItemCommand, int>
    {
        public readonly IUnitOfWork _unitOfWork;
        public UpdateCantityShoppingItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(UpdateCantityShoppingItemCommand command, CancellationToken cancellationToken)
        {
            int id = 0;
            var getShoppingItem = await _unitOfWork.ShoppingItems.GetShoppingItemByIds(command.ShoppingCartId, command.ProductId);
            var getProduct = await _unitOfWork.Products.GetByIdAsync(command.ProductId);

            if(getShoppingItem != null && getProduct != null)
            {
                if(command.Cantity == getShoppingItem.Cantity.Value)
                {
                    return getShoppingItem.ShoppingCartId;
                    //daca sunt egale cantitatile sa nu facem nimic
                }

                var getShoppingCart = await _unitOfWork.ShoppingCarts.GetCartByIdAsync(getShoppingItem.ShoppingCartId);
                var getBuyer = await _unitOfWork.Buyers.GetByIdAsync(command.BuyerId);
                if (command.Cantity != 0)
                {
                                     
                    if(getShoppingCart != null)
                    {
                        var totalPrice = 0.0;
                        //var buyerTotalBalance = 0.0;
                        if (command.Cantity > getShoppingItem.Cantity.Value)
                        {
                            totalPrice = getShoppingCart.TotalPrice.Value + (command.Cantity - getShoppingItem.Cantity.Value) * getProduct.Price.Value;
                            //buyerTotalBalance = getBuyer.Balance.Value - (command.Cantity - getShoppingItem.Cantity.Value) * getProduct.Price.Value;
                        }
                        else
                        {
                            totalPrice = getShoppingCart.TotalPrice.Value - (getShoppingItem.Cantity.Value - command.Cantity) * getProduct.Price.Value;
                            //buyerTotalBalance = getBuyer.Balance.Value + (getShoppingItem.Cantity.Value - command.Cantity) * getProduct.Price.Value;
                        }

                        getShoppingItem.Cantity = new Cantity(command.Cantity);
                        getShoppingCart.TotalPrice = new Price(totalPrice);        
                        //getBuyer.Balance = new Balance(buyerTotalBalance);

                        id = getShoppingItem.ShoppingCartId;
                    }
                    else
                    {
                        id = -3;
                    }
                   
                }
                else
                {
                    //stergere shopping item dupa acel produs sau cartul in sine daca era ultimul element din lista de itemuri pentru acel cart
                    var totalPrice = 0.0;
                    totalPrice =  getShoppingItem.Cantity.Value * getProduct.Price.Value;
                   
                    var itemsInCart = await _unitOfWork.ShoppingItems.GetAllShoppingItemsByShoppingCartId(getShoppingCart.Id);
                    if(itemsInCart.Count > 1)
                    {
                        getShoppingCart.TotalPrice = new Price(getShoppingCart.TotalPrice.Value - totalPrice);
                        _unitOfWork.ShoppingItems.Delete(getShoppingItem);
                    }
                    else
                    {
                        _unitOfWork.ShoppingCarts.Delete(getShoppingCart);
                    } 
                    
                    //getBuyer.Balance = new Balance(getBuyer.Balance.Value + totalPrice);
                    id = -4;

                }

                await _unitOfWork.CommitAsync(cancellationToken);
            }
            else
            {
                id = -3;
            }

            return id;
        }
    }
}
