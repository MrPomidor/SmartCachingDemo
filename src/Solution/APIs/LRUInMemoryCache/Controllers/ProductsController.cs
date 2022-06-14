using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reusables.Caching;
using Reusables.Storage;

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
            var product = await _productsCache.TryGet(id);
            if (product != null)
                return Ok(product);

            product = await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (product == null)
                return NotFound();

            await _productsCache.Set(id, product);

            return Ok(product);
        }
    }
}
