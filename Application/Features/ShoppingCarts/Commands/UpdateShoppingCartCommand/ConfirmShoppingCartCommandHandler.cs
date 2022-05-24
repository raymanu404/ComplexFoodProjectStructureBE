using MediatR;
using Application.DtoModels.Order;
using Application.DtoModels.OrderItem;
using Application.Contracts.Persistence;
using Application.Features.Orders.Commands.CreateOrder;
using Domain.Models.Ordering;
using AutoMapper;
using Domain.Models.Enums;
using Domain.ValueObjects;



namespace Application.Features.ShoppingCarts.Commands.UpdateShoppingCartCommand
{
    
    public class ConfirmShoppingCartCommandHandler : IRequestHandler<ConfirmShoppingCartCommand, string>
    {
        const double PRICE_PER_COUPON = 10.0;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ConfirmShoppingCartCommandHandler(IUnitOfWork unitOfWork,IMediator mediator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _mapper = mapper;

        }

        public async Task<string> Handle(ConfirmShoppingCartCommand command, CancellationToken cancellationToken)
        {
            var returnMessage = "";
            int orderIdInCaseOfException = 0;
            var totalPriceInCaseOfException = 0.0;
            try
            {
                var getBuyer = await _unitOfWork.Buyers.GetByIdAsync(command.BuyerId);
                var getCartByBuyerId = await _unitOfWork.ShoppingCarts.GetCartByBuyerIdAsync(command.BuyerId);
                if(getCartByBuyerId != null && getBuyer != null)
                {
                   
                    var discountTotalPrice = 0.0;
                    int discount = 0;
                    if(command.CouponCode.Length != 0 && !command.CouponCode.Equals("string"))
                    {
                        //verificam validitatea cuponului
                        var couponDto = await _unitOfWork.Coupons.GetByUniqueCodeAsync(new UniqueCode(command.CouponCode), command.BuyerId);
                        if (couponDto != null)
                        {
                            switch (couponDto.Type)
                            {
                                case TypeCoupons.TenProcent:
                                    discount = 10;
                                    break; 
                                case TypeCoupons.TwentyProcent:
                                    discount = 20;
                                    break; 
                                case TypeCoupons.ThirtyProcent:
                                    discount = 30;
                                    break;
                               
                            }

                            discountTotalPrice = getCartByBuyerId.TotalPrice.Value - (getCartByBuyerId.TotalPrice.Value * discount / 100) - PRICE_PER_COUPON;
                            _unitOfWork.Coupons.Delete(couponDto);
                           
                        }
                        else
                        {
                            discountTotalPrice = getCartByBuyerId.TotalPrice.Value;
                        }

                    }
                    else
                    {
                        discountTotalPrice = getCartByBuyerId.TotalPrice.Value;
                    }

                    var newOrder = new OrderDto
                    {
                        TotalPrice = Math.Round(discountTotalPrice, 2),
                        DatePlaced = DateTime.Now,
                        Status = OrderStatus.Placed,
                        Discount = discount,
                        Code = getCartByBuyerId.Code.Value,
                        BuyerId = command.BuyerId
                        
                    };
                    totalPriceInCaseOfException = newOrder.TotalPrice;
                    var createOrderCommand = new CreateOrderCommand
                    {
                        Order = newOrder

                    };

                    var responseOrderId = await _mediator.Send(createOrderCommand);
                    orderIdInCaseOfException = responseOrderId;
                    if (responseOrderId > 0)
                    {

                        //inainte sa stergem cartul nostru ar trebui sa creeam orderItems-urile pentru buyer-ul nostru in functie de ShoppingItems
                        var getShoppingItemsByCartIdList = await _unitOfWork.ShoppingItems.GetAllShoppingItemsByShoppingCartId(getCartByBuyerId.Id);
                        foreach(var getShoppingItem in getShoppingItemsByCartIdList)
                        {
                            var getProduct = await _unitOfWork.Products.GetByIdAsync(getShoppingItem.ProductId);
                            if(getProduct != null)
                            {
                                var newOrderItemDto = new OrderItemDto
                                {
                                    Cantity = getShoppingItem.Cantity.Value,
                                    Category = getProduct.Category,
                                    Title = getProduct.Title,
                                    Description = getProduct.Description,
                                    Price = getProduct.Price.Value,
                                    Image = getProduct.Image,
                                    OrderId = responseOrderId

                                };

                                var newOrderItem = _mapper.Map<OrderItem>(newOrderItemDto);
                                await _unitOfWork.OrderItems.AddAsync(newOrderItem);

                            }
                        }
                        
                        //stergem  cartul doar daca order-ul si itemele din order s au creat cu succes!;
                        _unitOfWork.ShoppingCarts.Delete(getCartByBuyerId);

                        //actualizam Balance buyer-lui
                        getBuyer.Balance = new Balance(getBuyer.Balance.Value - newOrder.TotalPrice);
                        await _unitOfWork.CommitAsync(cancellationToken);
                        returnMessage = $"OrderCode:{newOrder.Code}";

                    }
                    else
                    {
                        returnMessage = "Failed to create your order!";
                    }
                }
                else
                {
                    returnMessage = "Buyer doesn't exists in cart!";
                }

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                returnMessage = "Upss, an exception had occurred in confirmation of your cart!";
                var getBuyer = await _unitOfWork.Buyers.GetByIdAsync(command.BuyerId);
                var order = await _unitOfWork.Orders.GetByIdAsync(orderIdInCaseOfException);
                if(order != null && getBuyer != null)
                {
                    getBuyer.Balance = new Balance(getBuyer.Balance.Value + totalPriceInCaseOfException);
                    _unitOfWork.Orders.DeleteOrder(order);
                    await _unitOfWork.CommitAsync(cancellationToken);
                }           

            }

            return returnMessage;
        }
    }
}
