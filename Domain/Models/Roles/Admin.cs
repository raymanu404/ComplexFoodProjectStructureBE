namespace Domain.Models.Roles;
using Domain.ValueObjects;
public class Admin
{
    public int Id { get; set; }
    public Email Email { get; set; }
    public Password Password { get; set; }
    public Name FirstName { get; set; }
    public Name LastName { get; set; }
}