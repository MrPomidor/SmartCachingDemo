using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Reusables.Storage.Entities;

namespace Reusables.Storage
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
