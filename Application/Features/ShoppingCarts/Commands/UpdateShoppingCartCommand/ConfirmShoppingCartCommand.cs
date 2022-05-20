using MediatR;
using Application.DtoModels.Cart;

namespace Application.Features.ShoppingCarts.Commands.UpdateShoppingCartCommand
{
    public class ConfirmShoppingCartCommand : IRequest<string>
    {
        public int BuyerId { get; set; }

        //poate sa fie unul de la cupoane 10 20 30 (tipul cuponului) si atunci va fi scazut din pretul total in functie de acest discount - pretul initial al cuponului
        public string CouponCode { get; set; } = string.Empty;
        
    }
}
