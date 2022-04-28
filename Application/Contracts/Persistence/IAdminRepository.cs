using Domain.Models.Roles;

namespace Application.Contracts.Persistence
{
    public interface IAdminRepository
    {
        Task CreateAdminAsync(Admin admin);
    }
}
