using System.Threading.Tasks;
using Reusables.Storage.Entities;

namespace Reusables.Caching
{
    public interface IProductsCache
    {
        ValueTask<Product> TryGet(long productId);
        ValueTask Set(long productId, Product value);
    }
}
