using Domain.Models.Roles;
using Domain.ValueObjects;

namespace Application.Contracts.Persistence;

public interface IBuyerRepository
{
    Task AddAsync(Buyer buyer);
    void Delete(Buyer buyer);
    Task<Buyer?> GetByIdAsync(int id);
    Task<bool> GetBuyerByEmail(Email email);
    Task<List<Buyer>> GetAllAsync();
    Task<Buyer?> LoginBuyer(Email email, Password password);

    Task<Buyer?> GetBuyerByEmailAsync(Email email);
}