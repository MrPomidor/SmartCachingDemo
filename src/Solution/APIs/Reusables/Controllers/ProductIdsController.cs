using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reusables.Controllers.Models;
using Reusables.Storage;

namespace Reusables.Controllers
{
    public class ProductIdsController : Controller
    {
        private readonly ProductsContext _dbContext;
        public ProductIdsController(ProductsContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("api/products/ids")]
        [HttpGet]
        public async Task<IActionResult> GetProductIds([FromQuery] int page = 1, [FromQuery] int take = 10_000, CancellationToken cancellationToken = default)
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
    }
}
