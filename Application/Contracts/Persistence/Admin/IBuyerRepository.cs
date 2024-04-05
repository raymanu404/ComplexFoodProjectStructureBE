using Domain.Models.Roles;
using Domain.ValueObjects;

namespace Application.Contracts.Persistence.Admin;

public interface IBuyerRepository
{
    void Delete(Buyer buyer);
    Task<Buyer?> GetByIdAsync(int id);
    Task<bool> GetBuyerByEmail(Email email);
    Task<List<Buyer>> GetAllAsync();
    Task<Buyer?> GetBuyerByEmailAsync(Email email);
}