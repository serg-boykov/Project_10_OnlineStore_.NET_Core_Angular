using System.Reflection;
using System.Text.Json;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                if (!context.ProductBrands.Any())
                {
                    string PathBrands = path + @"/Data/SeedData/brands.json";
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
                    string PathTypes = path + @"/Data/SeedData/types.json";
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
                    string PathProducts = path + @"/Data/SeedData/products.json";
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

                if (!context.DeliveryMethods.Any())
                {
                    string PathProducts = path + @"/Data/SeedData/delivery.json";
                    var dmData = File.ReadAllText(PathProducts);

                    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);

                    foreach (var item in methods)
                    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        context.DeliveryMethods.Add(item);
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