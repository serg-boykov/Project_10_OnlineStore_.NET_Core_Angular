using System.Text.Json;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    const string PathBrands = "../Infrastructure/Data/SeedData/brands.json";
                    var brandsData = File.ReadAllText(PathBrands);

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var item in brands)
                    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        context.ProductBrands.Add(item);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any())
                {
                    const string PathTypes = "../Infrastructure/Data/SeedData/types.json";
                    var typesData = File.ReadAllText(PathTypes);

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types)
                    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        context.ProductTypes.Add(item);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    const string PathProducts = "../Infrastructure/Data/SeedData/products.json";
                    var productsData = File.ReadAllText(PathProducts);

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in products)
                    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        context.Products.Add(item);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}