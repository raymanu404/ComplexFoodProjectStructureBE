using Domain.Models.Roles;
using Domain.ValueObjects;

namespace ApplicationAdmin.Contracts.Persistence;

public interface IBuyerRepository
{
    void Delete(Buyer buyer);
    Task<Buyer?> GetByIdAsync(int id);
    Task<bool> GetBuyerByEmail(Email email);
    Task<List<Buyer>> GetAllAsync();
    Task<Buyer?> GetBuyerByEmailAsync(Email email);
}