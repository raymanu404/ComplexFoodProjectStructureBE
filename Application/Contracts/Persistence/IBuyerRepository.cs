using Domain.Models.Roles;
using Application.DtoModels.Buyer;
using Domain.ValueObjects;

namespace Application.Contracts.Persistence;

public interface IBuyerRepository
{
    Task AddAsync(Buyer buyer);
    void Update(BuyerDto buyer);
    void Delete(Buyer buyer);
    Task<Buyer?> GetByIdAsync(int id);
    Task<List<Buyer>> GetAllAsync();
    Task<BuyerDto?> LoginBuyer(Email email, Password password);
}