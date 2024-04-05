using Domain.Models.Shopping;
using Application.DtoModels.Product;

namespace Application.Contracts.Persistence.Customer;

public interface IProductRepository
{
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    void Delete(Product product);
    Task<Product?> GetByIdAsync(int id);
    Task<List<Product>> GetAllAsync();
}

