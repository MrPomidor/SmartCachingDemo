using System;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore.Migrations;
using Reusables.Storage;

#nullable disable

namespace InMemoryLRU.Storage.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var products = ProductsContextSeeder.GetProducts();

            foreach (var productsChunk in products.Chunk(1_000))
            {
                var dataChunkStr = string.Join("," + Environment.NewLine, productsChunk.Where(x => x != null).Select(product => 
                    $"(" +
                    $"'{product.Name.Replace("'", "''")}', " +
                    $"'{product.Description.Replace("'", "''")}', " +
                    $"'{product.LongDescription.Replace("'", "''")}', " +
                    $"'{product.Category.Replace("'", "''")}', " +
                    $"'{product.Manufacturer.Replace("'", "''")}', " +
                    $"{product.Price.ToString(CultureInfo.InvariantCulture)}, " +
                    $"'{product.Tags.Replace("'", "''")}', " +
                    $"'{product.DateCreated.ToString("yyyy-MM-dd")}', " +
                    $"'{product.DateUpdated.ToString("yyyy-MM-dd")}'" +
                    $")"));

                migrationBuilder.Sql($"INSERT INTO Products VALUES {dataChunkStr};", suppressTransaction: true);
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // no down migration is needed
        }
    }
}
