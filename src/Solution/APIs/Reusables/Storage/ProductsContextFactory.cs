using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Reusables.Storage
{
    public class ProductsContextFactory : IDesignTimeDbContextFactory<ProductsContext>
    {
        public ProductsContext CreateDbContext(string[] args)
        {
            var condifurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false);

            var config = condifurationBuilder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<ProductsContext>();

            // We need this line to disable extra logging to console. Without this, program will log every entire SQL command to package manager console
            // As entire migration is written using .Sql(), we need to disable this to reduce execution time
            optionsBuilder.UseLoggerFactory(new LoggerFactory(Enumerable.Empty<ILoggerProvider>(), new LoggerFilterOptions { MinLevel = LogLevel.Warning }));

            optionsBuilder.UseSqlServer(config.GetConnectionString("ProductsDB"));

            return new ProductsContext(optionsBuilder.Options);
        }
    }
}
