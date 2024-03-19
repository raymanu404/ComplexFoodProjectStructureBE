
using Domain.ValueObjects;

namespace Domain.Models.Abstractions;
public class User
{
    public int Id { get; set; }
    public Email Email { get; set; }
    public Password Password { get; set; }
    public Name FirstName { get; set; }
    public Name LastName { get; set; }
}
