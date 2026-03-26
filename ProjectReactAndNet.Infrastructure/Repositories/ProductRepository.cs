using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using ProjectReactAndNet.Domain.Interfaces;
using ProjectReactAndNet.Infrastructure.Data;

namespace ProjectReactAndNet.Infrastructure.Repositories
{
    public class ProductRepository(AppDbContext db) : IProductRepository
    {
        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await db.Products.OrderByDescending(p => p.UpdatedAt).ToListAsync();

        public async Task<Product?> GetByIdAsync(int id) =>
            await db.Products.FindAsync(id);

        public async Task AddAsync(Product product)
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await db.Products.FindAsync(id);
            if (product is not null)
            {
                db.Products.Remove(product);
                await db.SaveChangesAsync();
            }
        }
    }
}
