using Domain.Models.Shopping;

namespace ApplicationAdmin.Contracts.Persistence;

public interface IProductRepository
{
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    void Delete(Product product);
    Task<Product?> GetByIdAsync(int id);
    Task<List<Product>> GetAllAsync();
}

