using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Reusables.Caching.Redis
{
    public interface IRedisManager : IDisposable
    {
        Task<RedisValue> GetValue(RedisKey key);
        Task SetValue(RedisKey key, RedisValue value, TimeSpan timeToLive);
        Task<long> GetUsedMemoryBytes();
        Task SetMaxMemory(int maxMemoryMb);
        Task ClearCache();
    }
}
