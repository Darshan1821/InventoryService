using InventoryService.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Data
{
    public class ProductDbSeeder
    {
        private readonly ILogger logger;

        public ProductDbSeeder(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger("ProductDbSeeder");
        }

        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var productsDb = serviceScope.ServiceProvider.GetService<InventoryDbContext>();
                if (await productsDb.Database.EnsureCreatedAsync())
                {
                    if (!await productsDb.Product.AnyAsync())
                    {
                        await InsertInventorySampleData(productsDb);
                    }
                }
            }
        }

        public async Task InsertInventorySampleData(InventoryDbContext db)
        {
            var products = GetProducts();
            db.Product.AddRange(products);

            try
            {
                int numAffected = await db.SaveChangesAsync();
                logger.LogInformation($"Saved {numAffected} products");
            }
            catch (Exception exp)
            {
                logger.LogError($"Error in {nameof(ProductDbSeeder)}: " + exp.Message);
                throw;
            }

        }

        private List<Product> GetProducts()
        {
            var productList = new List<Product>()
            {
                new Product() { Name="Lettuce", Price=10.5, Quantity=50, Type = ProductType.LeafyGreen },
                new Product() { Name="Cabbage", Price=20, Quantity=100, Type = ProductType.Cruciferous },
                new Product() { Name="Pumpkin", Price=30, Quantity=30, Type = ProductType.Marrow },
                new Product() { Name="Cauliflower", Price=10, Quantity=25, Type = ProductType.Cruciferous },
                new Product() { Name="Zucchini", Price=20.5, Quantity=50, Type = ProductType.Marrow },
                new Product() { Name="Yam", Price=30, Quantity=50, Type = ProductType.Root },
                new Product() { Name="Spinach", Price=10, Quantity=100, Type = ProductType.LeafyGreen },
                new Product() { Name="Broccoli", Price=20.2, Quantity=75, Type = ProductType.Cruciferous },
                new Product() { Name="Garlic", Price=30, Quantity=20, Type = ProductType.LeafyGreen },
                new Product() { Name="Silverbeet", Price=10, Quantity=50, Type = ProductType.Marrow }
            };

            return productList;
        }
    }
}
