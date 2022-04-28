using Domain.ValueObjects;

namespace Application.DtoModels.Buyer
{
    public class BuyerDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }

    }
}
