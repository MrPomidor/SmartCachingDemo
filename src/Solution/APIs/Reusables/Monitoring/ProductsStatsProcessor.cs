using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reusables.Storage;

namespace Reusables.Monitoring
{
    public class ProductsStatsProcessor : BackgroundService
    {
        private static readonly TimeSpan LoopInterval = TimeSpan.FromSeconds(30); // TODO config ?

        private readonly IProductsStatsAggregator _statsAggregator;
        private readonly IServiceProvider _serviceProvider;

        public ProductsStatsProcessor(
            IProductsStatsAggregator statsAggregator,
            IServiceProvider serviceProvider)
        {
            _statsAggregator = statsAggregator;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                var dateNow = DateTime.UtcNow;

                var stats = _statsAggregator.Flush();

                var matchesCount = stats.Values.OrderByDescending(x => x).ToArray();
                var totalMatches = Sum(matchesCount);
                var matchesCountMemory = matchesCount.AsMemory();

                var totalProductsCount = await GetProductsCount(stoppingToken);

                var range_5percent = GetPercent(matchesCountMemory, 5, totalMatches);
                var range_10percent = GetPercent(matchesCountMemory, 10, totalMatches);
                var range_15percent = GetPercent(matchesCountMemory, 15, totalMatches);
                var range_20percent = GetPercent(matchesCountMemory, 20, totalMatches);
                var range_30percent = GetPercent(matchesCountMemory, 30, totalMatches);
                var range_50percent = GetPercent(matchesCountMemory, 50, totalMatches);

                ProductsStatsEventSource.Log.SetStats(
                    range_5percent,
                    range_10percent,
                    range_15percent,
                    range_20percent,
                    range_30percent,
                    range_50percent
                    );

                var range_5percentFromTotal = GetPercentFromTotal(matchesCountMemory, totalProductsCount, 5, totalMatches);
                var range_10percentFromTotal = GetPercentFromTotal(matchesCountMemory, totalProductsCount, 10, totalMatches);
                var range_15percentFromTotal = GetPercentFromTotal(matchesCountMemory, totalProductsCount, 15, totalMatches);
                var range_20percentFromTotal = GetPercentFromTotal(matchesCountMemory, totalProductsCount, 20, totalMatches);
                var range_30percentFromTotal = GetPercentFromTotal(matchesCountMemory, totalProductsCount, 30, totalMatches);
                var range_50percentFromTotal = GetPercentFromTotal(matchesCountMemory, totalProductsCount, 50, totalMatches);

                ProductsStatsEventSource.Log.SetStats(
                    range_5percentFromTotal,
                    range_10percentFromTotal,
                    range_15percentFromTotal,
                    range_20percentFromTotal,
                    range_30percentFromTotal,
                    range_50percentFromTotal
                    );

                var dateAfter = DateTime.UtcNow;
                var timeToWait = LoopInterval - (dateAfter - dateNow);
                await Task.Delay(timeToWait, stoppingToken);
            }
        }

        private double GetPercent(Memory<int> allMatches, int percent, int totalMatches)
        {
            (int start, int length) percentageRange = (0, (int)Math.Floor(allMatches.Length * (double)percent / 100));
            var segment = allMatches.Span.Slice(percentageRange.start, percentageRange.length);

            var percentageFromTotal = GetPercentFromTotal(segment, totalMatches);
            return percentageFromTotal;
        }

        private double GetPercentFromTotal(Memory<int> allMatches, int totalProductsCount, int percent, int totalMatches)
        {
            var smallLength = allMatches.Length;
            var lengthFromTotal = (int)Math.Floor(totalProductsCount * (double)percent / 100);
            (int start, int length) percentageRange = (0, lengthFromTotal > smallLength ? smallLength : lengthFromTotal);
            var segment = allMatches.Span.Slice(percentageRange.start, percentageRange.length);

            var percentageFromTotal = GetPercentFromTotal(segment, totalMatches);
            return percentageFromTotal;
        }

        private double GetPercentFromTotal(Span<int> segment, int totalMatches)
        {
            var segmentTotal = Sum(segment);
            var percentageFromTotal = segmentTotal / totalMatches * 100;
            return percentageFromTotal;
        }

        private int Sum(Span<int> values)
        {
            int aggregator = 0;
            for (int i =0; i< values.Length; i++)
            {
                aggregator += values[i];
            }
            return aggregator;
        }

        private async Task<int> GetProductsCount(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            using (var dbContext = scope.ServiceProvider.GetRequiredService<ProductsContext>())
            {
                return await dbContext.Products.CountAsync(stoppingToken);
            }
        }
    }
}
