using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContexSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                //Seeding Product Brand Data
                if (!context.ProductBrand.Any())
                {
                    var brandData =
                    File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                    foreach (var items in brands)
                    {
                        context.ProductBrand.Add(items);
                    }

                    await context.SaveChangesAsync();
                }

                //Seeding Product Type Data
                if (!context.ProductType.Any())
                {
                    var typeData =
                    File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);

                    foreach (var items in types)
                    {
                        context.ProductType.Add(items);
                    }

                    await context.SaveChangesAsync();
                }

                //Seeding Product Data
                if (!context.Product.Any())
                {
                    var productData =
                    File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productData);

                    foreach (var items in products)
                    {
                        context.Product.Add(items);
                    }

                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContexSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}