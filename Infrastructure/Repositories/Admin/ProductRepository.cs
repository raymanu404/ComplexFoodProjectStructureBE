using Microsoft.EntityFrameworkCore;
using Domain.Models.Shopping;
using ApplicationAdmin.Contracts.Persistence;
using ApplicationAdmin.DtoModels.Product;
using Domain.ValueObjects;

namespace Infrastructure.Repositories.Admin
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

        public async Task<List<Product>> GetAllAsync(string? searchTerm, CancellationToken cancellationToken)
        {
            IQueryable<Product> productsQuery = _context.Products;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery.Where(item => item.Title.ToLower().Contains(searchTerm));
            }

            return await productsQuery.ToListAsync(cancellationToken);
        }

        public IQueryable<Product> GetQueryable()
        {
            IQueryable<Product> productsQuery = _context.Products;
            return productsQuery;
        }

        public async Task<Product?> GetByIdAsync(int id) => await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();

    }
}
