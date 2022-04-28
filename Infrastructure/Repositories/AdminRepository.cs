using Application.Contracts.Persistence;
using Domain.Models.Roles;

namespace Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationContext _context;
        public AdminRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task CreateAdminAsync(Admin admin) => await _context.Admins.AddAsync(admin);
    }
}
