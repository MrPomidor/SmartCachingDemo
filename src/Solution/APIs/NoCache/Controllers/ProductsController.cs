using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InMemoryLRU.Controllers.Models;
using InMemoryLRU.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InMemoryLRU.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly ProductsContext _dbContext;
        public ProductsController(ProductsContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("ids")]
        public async Task<IActionResult> GetProductIds([FromQuery]int page = 1, [FromQuery]int take = 10_000, CancellationToken cancellationToken = default)
        {
            var count = await _dbContext.Products.LongCountAsync(cancellationToken);
            var idsPage = await _dbContext.Products
                .Select(x => x.Id)
                .OrderBy(x => x)
                .Skip((page - 1) * take).Take(take)
                .ToArrayAsync(cancellationToken);
            return Ok(new IdsPage
            {
                Items = idsPage,
                TotalCount = count
            });
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetProduct(long id, CancellationToken cancellationToken = default)
        {
            var product = await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
    }
}
