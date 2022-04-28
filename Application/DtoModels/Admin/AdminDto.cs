using Domain.ValueObjects;

namespace Application.DtoModels.Admin
{
    public class AdminDto
    {
        public Email Email { get; set; }
        public Password Password { get; set; }
        public Name FirstName { get; set; }
        public Name LastName { get; set; }
    }
}
