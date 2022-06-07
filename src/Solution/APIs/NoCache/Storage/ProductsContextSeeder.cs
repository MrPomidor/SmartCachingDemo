using System;
using System.Collections.Generic;
using Bogus;
using InMemoryLRU.Storage.Entities;

namespace InMemoryLRU.Storage
{
    public static class ProductsContextSeeder
    {
        public static List<Product> GetProducts()
        {
            const int productsCount = 1_000_000;

            long productIdCounter = 1;

            var fakeProductFactory = new Faker<Product>()
                .StrictMode(true)
                .RuleFor(x => x.Id, x => productIdCounter++)
                .RuleFor(x => x.Name, x => x.Commerce.Product() + " " + x.Random.Guid().ToString().Substring(0,8))
                .RuleFor(x => x.Description, x => x.Commerce.ProductDescription() + " " + x.Random.Guid().ToString().Substring(0, 8))
                .RuleFor(x => x.LongDescription, x => x.Lorem.Lines(x.Random.Number(1, 3)))
                .RuleFor(x => x.Category, x => x.Commerce.Categories(1)[0])
                .RuleFor(x => x.Manufacturer, x => x.Company.CompanyName())
                .RuleFor(x => x.Price, x => x.Random.Decimal(1, 10_000))
                .RuleFor(x => x.Tags, x => string.Join(",", x.Random.WordsArray(x.Random.Int(1, 6))))
                .RuleFor(x => x.DateCreated, x => x.Date.Past())
                .RuleFor(x => x.DateUpdated, (x, i) => x.Date.Between(i.DateCreated, DateTime.Now));

            return fakeProductFactory.Generate(productsCount);
        }
    }
}
