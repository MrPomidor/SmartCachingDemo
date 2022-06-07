using System.Diagnostics.CodeAnalysis;
using InMemoryLRU.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace InMemoryLRU.Storage
{
    public class ProductsContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }

        protected ProductsContext()
        {
        }

        public ProductsContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
