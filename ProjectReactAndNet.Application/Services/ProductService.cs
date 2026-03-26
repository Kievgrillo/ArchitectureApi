using MyApp.Domain.Entities;
using ProjectReactAndNet.Domain.Interfaces;

namespace ProjectReactAndNet.Application.Services
{
    public class ProductService(IProductRepository repository)
    {
        public Task<IEnumerable<Product>> GetAllAsync() =>
            repository.GetAllAsync();

        public Task<Product?> GetByIdAsync(int id) =>
            repository.GetByIdAsync(id);

        public Task CreateAsync(Product product)
        {
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;
            return repository.AddAsync(product);
        }

        public Task UpdateAsync(Product product)
        {
            product.UpdatedAt = DateTime.UtcNow;
            return repository.UpdateAsync(product);
        }

        public Task DeleteAsync(int id) =>
            repository.DeleteAsync(id);
    }
}
