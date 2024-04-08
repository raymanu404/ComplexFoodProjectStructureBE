using Domain.Models.Shopping;

namespace ApplicationAdmin.Contracts.Persistence;

public interface IProductRepository
{
    Task AddAsync(Product product);
    void Delete(Product product);
    Task<Product?> GetByIdAsync(int id);
    Task<List<Product>> GetAllAsync(string? searchTerm, CancellationToken cancellationToken);
    IQueryable<Product> GetQueryable();
}