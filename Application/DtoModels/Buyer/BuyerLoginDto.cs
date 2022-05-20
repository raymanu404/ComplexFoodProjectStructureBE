using Domain.ValueObjects;

namespace Application.DtoModels.Buyer
{
    public class BuyerLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
