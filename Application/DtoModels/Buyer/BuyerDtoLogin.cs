using Domain.ValueObjects;

namespace Application.DtoModels.Buyer
{
    public class BuyerDtoLogin
    {
        public Email Email { get; set; }
        public Password Password { get; set; }
    }
}
