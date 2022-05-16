using Microsoft.EntityFrameworkCore;
using Application.Contracts.Persistence;
using Domain.Models.Shopping;
using Application.DtoModels.Product;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product) => await _context.Products.AddAsync(product);

        public void Delete(Product product) => _context.Products.Remove(product);

        public async Task<List<Product>> GetAllAsync() => await _context.Products.ToListAsync();

        public async Task<Product?> GetByIdAsync(int id) => await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();

        public Task UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
