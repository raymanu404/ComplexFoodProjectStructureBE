using System.Collections.Generic;


namespace Application.DtoModels.Buyer
{
    public class BuyerDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public bool Confirmed { get; set; }
        public double Balance { get; set; }
        public string ConfirmationCode { get; set; }
        public List<Domain.Models.Shopping.Coupon> Coupons { get; set; }


    }
}
