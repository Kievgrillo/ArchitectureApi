using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;

namespace ProjectReactAndNet.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");

                entity.HasData(
                    new Product { Id = 1, Name = "Notebook Dell XPS", Price = 7499.90m, Stock = 10, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Product { Id = 2, Name = "Monitor LG 27\"", Price = 1899.00m, Stock = 3, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Product { Id = 3, Name = "Teclado Mecânico", Price = 349.90m, Stock = 0, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                );
            });
        }
    }
}
