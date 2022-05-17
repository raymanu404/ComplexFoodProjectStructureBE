using MediatR;
using Application.Contracts.Persistence;
using Domain.ValueObjects;

namespace Application.Features.ShoppingItems.Commands
{
    public class UpdateCantityShoppingItemCommandHandler : IRequestHandler<UpdateCantityShoppingItemCommand, string>
    {
        public readonly IUnitOfWork _unitOfWork;
        public UpdateCantityShoppingItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(UpdateCantityShoppingItemCommand command, CancellationToken cancellationToken)
        {
            string returnMessage = "";
            var getShoppingItem = await _unitOfWork.ShoppingItems.GetShoppingItemByIds(command.ShoppingCartId, command.ProductId);
            var getProduct = await _unitOfWork.Products.GetByIdAsync(command.ProductId);

            if(getShoppingItem != null && getProduct != null)
            {
                var getShoppingCart = await _unitOfWork.ShoppingCarts.GetCartByIdAsync(getShoppingItem.ShoppingCartId);
                var getBuyer = await _unitOfWork.Buyers.GetByIdAsync(command.BuyerId);
                if (command.Cantity != 0)
                {
                                     
                    if(getShoppingCart != null)
                    {
                        var totalPrice = 0.0;
                        var buyerTotalBalance = 0.0;
                        if (command.Cantity > getShoppingItem.Cantity.Value)
                        {
                            totalPrice = getShoppingCart.TotalPrice.Value + (command.Cantity - getShoppingItem.Cantity.Value) * getProduct.Price.Value;
                            buyerTotalBalance = getBuyer.Balance.Value - (command.Cantity - getShoppingItem.Cantity.Value) * getProduct.Price.Value;
                        }
                        else
                        {
                            totalPrice = getShoppingCart.TotalPrice.Value - (getShoppingItem.Cantity.Value - command.Cantity) * getProduct.Price.Value;
                            buyerTotalBalance = getBuyer.Balance.Value + (getShoppingItem.Cantity.Value - command.Cantity) * getProduct.Price.Value;
                        }

                        getShoppingItem.Cantity = new Cantity(command.Cantity);
                        getShoppingCart.TotalPrice = new Price(totalPrice);        
                        getBuyer.Balance = new Balance(buyerTotalBalance);
                        
                        returnMessage = "Your item was updated succssesfully";
                    }
                   
                }
                else
                {
                    //stergere shopping item dupa acel produs
                    var totalPrice = 0.0;
                    totalPrice = getShoppingCart.TotalPrice.Value - getShoppingItem.Cantity.Value * getProduct.Price.Value;
                    getShoppingCart.TotalPrice = new Price(totalPrice);
                    _unitOfWork.ShoppingItems.Delete(getShoppingItem);
                    returnMessage = "Item was deleted successesfully!";

                }

                await _unitOfWork.CommitAsync(cancellationToken);
            }
            else
            {
                returnMessage = "Item doesn't exists!";
            }

            return returnMessage;
        }
    }
}
