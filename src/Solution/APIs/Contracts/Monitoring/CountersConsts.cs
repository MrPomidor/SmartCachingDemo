namespace Contracts.Monitoring
{
    public static class CountersConsts
    {
        public static class EventSources
        {
            public const string InMemoryLRUProductsCache = "LRUDemo.Products.LRUCache.InMemory";
            public const string RedisLRUProductsCache = "LRUDemo.Products.LRUCache.Redis";
        }

        public static class Metrics
        {
            public static class InMemoryLRUProductsCache
            {
                public const string FetchCount = "fetch-count"; // amount of "matches", when item was successfully fetched from cache
                public const string AddCount = "add-count"; // amount of items added to cache
                public const string ExpireCount = "expire-count"; // amount of items fetched from cache, but expired
                public const string NotFoundCount = "notfound-count"; // amount of not successful get requests
            }

            public static class RedisLRUProductsCache
            {
                public const string AddCount = "add-count"; // amount of items added to cache
                public const string FetchCount = "fetch-count"; // amount of "matches", when item was successfully fetched from cache
                public const string NotFoundCount = "notfound-count"; // amount of not successful get requests
            }
        }
    }
}
