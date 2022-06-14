using System;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Reusables.Caching.Redis
{
    public class RedisManager : IRedisManager
    {
        private readonly IConnectionMultiplexer _connection;
        public RedisManager(string redisConnectionString)
        {
            _connection = ConnectionMultiplexer.Connect(redisConnectionString);
        }

        public async Task<RedisValue> GetValue(RedisKey key)
        {
            var database = _connection.GetDatabase();

            var redisValue = await database.StringGetAsync(key);
            return redisValue;
        }

        public async Task SetValue(RedisKey key, RedisValue value, TimeSpan timeToLive)
        {
            var database = _connection.GetDatabase();

            _ = await database.StringSetAsync(key, value, timeToLive);
        }

        public async Task<long> GetUsedMemoryBytes()
        {
            var database = _connection.GetDatabase();

            var redisValue = await database.ExecuteAsync("INFO");

            var usedMemoryBytesStr = ((string)redisValue)
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .FirstOrDefault(x => x.StartsWith("used_memory:"))
                ?.Split(":")[1];

            return long.Parse(usedMemoryBytesStr);
        }

        public async Task SetMaxMemory(int maxMemoryMb)
        {
            var database = _connection.GetDatabase();

            _ = await database.ExecuteAsync("CONFIG", "SET", "maxmemory", $"{maxMemoryMb}mb");
        }

        public async Task ClearCache()
        {
            var database = _connection.GetDatabase();

            _ = await database.ExecuteAsync("FLUSHALL", "sync");
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
