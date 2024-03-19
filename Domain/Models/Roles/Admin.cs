using Domain.Models.Abstractions;
using Domain.Models.Enums;
using Domain.ValueObjects;

namespace Domain.Models.Roles;
public class Admin : User
{
    public RoleNameEnum Role { get; set; } = RoleNameEnum.Admin;
    public DateTime CreatedAt { get; set; }
}
