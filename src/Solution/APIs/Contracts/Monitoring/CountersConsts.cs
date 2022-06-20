namespace Contracts.Monitoring
{
    public static class CountersConsts
    {
        public static class EventSources
        {
            public const string InMemoryLRUProductsCache = "LRUDemo.Products.LRUCache.InMemory";
            public const string RedisLRUProductsCache = "LRUDemo.Products.LRUCache.Redis";
            public const string ProductsRequestStats = "LRUDemo.Products.RequestStats";
        }

        public static class Metrics
        {
            public static class InMemoryLRUProductsCache
            {
                public const string RequestedCount = "requested-count"; // amount of all "get" calls
                public const string FetchCount = "fetch-count"; // amount of "matches", when item was successfully fetched from cache
                public const string AddCount = "add-count"; // amount of items added to cache
                public const string ExpireCount = "expire-count"; // amount of items fetched from cache, but expired
                public const string NotFoundCount = "notfound-count"; // amount of not successful get requests
            }

            public static class RedisLRUProductsCache
            {
                public const string RequestedCount = "requested-count"; // amount of all "get" calls
                public const string AddCount = "add-count"; // amount of items added to cache
                public const string FetchCount = "fetch-count"; // amount of "matches", when item was successfully fetched from cache
                public const string NotFoundCount = "notfound-count"; // amount of not successful get requests
            }

            public static class ProductsRequestStats
            {
                public const string WorkingSetItemsCount = "count-workingset";

                public const string RequestQuota_5Percent = "requestquota-five-percents";
                public const string RequestQuota_10Percent = "requestquota-ten-percents";
                public const string RequestQuota_15Percent = "requestquota-fifteen-percents";
                public const string RequestQuota_20Percent = "requestquota-twenty-percents";
                public const string RequestQuota_30Percent = "requestquota-thirty-percents";
                public const string RequestQuota_50Percent = "requestquota-fifty-percents";

                public const string TotalItemsCount = "count-totalitems";

                public const string RequestQuota_FromTotal_5Percent = "fromtota-requestquotal-five-percents";
                public const string RequestQuota_FromTotal_10Percent = "fromtotal-requestquota-ten-percents";
                public const string RequestQuota_FromTotal_15Percent = "fromtotal-requestquota-fifteen-percents";
                public const string RequestQuota_FromTotal_20Percent = "fromtotal-requestquota-twenty-percents";
                public const string RequestQuota_FromTotal_30Percent = "fromtotal-requestquota-thirty-percents";
                public const string RequestQuota_FromTotal_50Percent = "fromtotal-requestquota-fifty-percents";
            }
        }
    }
}
