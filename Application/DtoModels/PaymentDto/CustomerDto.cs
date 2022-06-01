
namespace Application.DtoModels.PaymentDto
{
    public class CustomerDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        //public string State { get; set; }
        public string AddressLine1 { get; set; }
        //public string PostalCode { get; set; }
        public string Phone { get; set; }
        public long Amount { get; set; }

    }
}
