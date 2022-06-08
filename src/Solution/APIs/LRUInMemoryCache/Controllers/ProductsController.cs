using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Reusables.Storage;
using Microsoft.EntityFrameworkCore;
using LRUInMemoryCache.Services;

namespace LRUInMemoryCache.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly ProductsContext _dbContext;
        private readonly IProductsCache _productsCache;
        public ProductsController(
            ProductsContext dbContext,
            IProductsCache productsCache
            )
        {
            _dbContext = dbContext;
            _productsCache = productsCache;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetProduct(long id, CancellationToken cancellationToken = default)
        {
            if (_productsCache.TryGet(id, out var product))
                return Ok(product);

            product = await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (product == null)
                return NotFound();

            _productsCache.Set(id, product);

            return Ok(product);
        }
    }
}
