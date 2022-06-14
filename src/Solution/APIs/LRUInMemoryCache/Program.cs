using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reusables.Caching;
using Reusables.Caching.InMemory;
using Reusables.Storage;

namespace LRUInMemoryCache
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();
            Configure(app);

            app.Run();
        }

        public static void ConfigureServices(IServiceCollection services, ConfigurationManager config)
        {
            services
                .AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
                    );

            services.AddDbContext<ProductsContext>((optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(config.GetConnectionString("ProductsDB"));
            });

            services.AddSingleton<IProductsCache, ProductsInMemoryCache>();
        }

        public static void Configure(WebApplication app)
        {
            app.MapControllers();
        }
    }
}