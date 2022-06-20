
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using NBomber.Configuration;
using NBomber.Contracts;
using NBomber.CSharp;
using Newtonsoft.Json;
using Serilog;

// application configuration
const string baseUrl = "https://localhost:44359/api/products";

// benchmark configuration

/*
    Default amount of items: 1_000_000 (one million)
    1 percent = 10_000
    5 percent = 50_000
    10 percent = 100_000
    50 percent = 500_000
 */

const int percentOfOftenRequestedItems = 1;
const int maxItemsCount = 1_000_000;

TimeSpan warmupTime = TimeSpan.FromSeconds(10);
TimeSpan timeToRun = TimeSpan.FromMinutes(10);



var productIds = await GetProductIds();

(var oftenRequestedFeed, var rareRequestedFeed) = GetIdDataFeeds(productIds, 
    oftenRequestedItemsPercent: percentOfOftenRequestedItems, 
    maxItems: maxItemsCount);

var clientFactory = ClientFactory.Create(
    name: "http_factory",
    clientCount: 30,
    initClient: (number, context) => Task.FromResult(new HttpClient()));

var oftenRequestedStep = Step.Create("Get Often Requested Items", clientFactory, oftenRequestedFeed, (context) =>
{
    return GetProduct(context.Client, id: context.FeedItem, context.Logger);
});

var rareRequestedStep = Step.Create("Get Rare Requested Items", clientFactory, rareRequestedFeed, (context) =>
{
    return GetProduct(context.Client, id: context.FeedItem, context.Logger);
});

var oftenRequestedScenario = ScenarioBuilder.CreateScenario("Often Requested Items", oftenRequestedStep)
    .WithWarmUpDuration(warmupTime)
    .WithLoadSimulations(
        LoadSimulation.NewKeepConstant(_copies: 10, _during: timeToRun)
    );

var rareRequestedScenario = ScenarioBuilder.CreateScenario("Rare Requested Items", rareRequestedStep)
    .WithWarmUpDuration(warmupTime)
    .WithLoadSimulations(
        LoadSimulation.NewKeepConstant(_copies: 10, _during: timeToRun)
    );

NBomberRunner
    .RegisterScenarios(oftenRequestedScenario, rareRequestedScenario)
    .WithReportFormats(ReportFormat.Html, ReportFormat.Md)
    .Run();

Console.WriteLine("Press any key ...");
Console.ReadKey();

return 0;



// utils
async Task<List<long>> GetProductIds()
{
    var client = new HttpClient();

    var resultIdsList = new List<long>(1_000_000);

    var pageNumber = 1;
    var itemsToFetch = 10_000;
    var countFetched = 0m;
    var totalCount = 0m;

    do
    {
        var url = $"{baseUrl}/ids?page={pageNumber}&take={itemsToFetch}";
        var result = await client.GetAsync(url);
        if (!result.IsSuccessStatusCode)
            throw new ApplicationException("Cannot fetch page");

        var resultBody = await result.Content.ReadAsStringAsync();
        var resultParsed = JsonConvert.DeserializeObject<IdsPage>(resultBody);

        resultIdsList.AddRange(resultParsed.Items);

        pageNumber++;

        totalCount = resultParsed.TotalCount;
        countFetched += resultParsed.Items.LongLength;
    }
    while (countFetched < totalCount);

    return resultIdsList;
}

(IFeed<long> oftenRequestedIdsFeed, IFeed<long> rareRequestIdsFeed) GetIdDataFeeds(List<long> ids, int oftenRequestedItemsPercent, int maxItems = 1_000_000)
{
    int oftenRequestedItemsCount = (int)(Math.Min(ids.Count, maxItems) * ((double)oftenRequestedItemsPercent / 100));
    var rareRequestedItemsCount = Math.Min(ids.Count, maxItems) - oftenRequestedItemsCount;

    var oftenRequestedItems = ids.GetRange(0, oftenRequestedItemsCount);
    var rareRequestedItems = ids.GetRange(oftenRequestedItemsCount - 1, rareRequestedItemsCount);

    Console.WriteLine($"Often requested items count: {oftenRequestedItemsCount}");
    Console.WriteLine($"Rest items count: {rareRequestedItemsCount}");

    var oftenRequestedDataFeed = Feed.CreateRandom("oftenRequestedItems", oftenRequestedItems);
    var rareRequestedDataFeed = Feed.CreateRandom("rareRequestedItems", rareRequestedItems);

    return (oftenRequestedDataFeed, rareRequestedDataFeed);
}

async Task<Response> GetProduct(HttpClient client, long id, ILogger logger = null)
{
    var response = await client.GetAsync($"{baseUrl}/{id}");
    if (response.IsSuccessStatusCode)
    {
        var bytes = await response.Content.ReadAsByteArrayAsync();
        return Response.Ok(payload: bytes, statusCode: (int)response.StatusCode);
    }
    else
    {
        if (logger != null)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            logger.Error(responseContent);
        }
        return Response.Fail(statusCode: (int)response.StatusCode, error: response.ReasonPhrase);
    }
}

public class IdsPage
{
    public long[] Items { get; set; }
    public long TotalCount { get; set; }
}