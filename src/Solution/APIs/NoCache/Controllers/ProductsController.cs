using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reusables.Monitoring;
using Reusables.Storage;

namespace InMemoryLRU.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly ProductsContext _dbContext;
        private readonly IProductsStatsAggregator _productsStatsAggregator;
        public ProductsController(ProductsContext dbContext, IProductsStatsAggregator productsStatsAggregator)
        {
            _dbContext = dbContext;
            _productsStatsAggregator = productsStatsAggregator;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetProduct(long id, CancellationToken cancellationToken = default)
        {
            var product = await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (product == null)
                return NotFound();

            _productsStatsAggregator.ProductRequested(id);
            return Ok(product);
        }
    }
}
