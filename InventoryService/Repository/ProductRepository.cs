using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryService.Data;
using InventoryService.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InventoryService.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryDbContext context;
        private readonly ILogger logger;

        public ProductRepository(InventoryDbContext inventoryDbContext,
                                 ILoggerFactory loggerFactory)
        {
            context = inventoryDbContext;
            logger = loggerFactory.CreateLogger("ProductRepository");
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await context.Product.FirstOrDefaultAsync(p => p.Id == id);
            context.Remove(product);
            try
            {
                return (await context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception exp)
            {
                logger.LogError($"Error in {nameof(DeleteProductAsync)}: " + exp.Message);
            }
            return false;
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await context.Product.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await context.Product.OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<PagingResult<Product>> GetProductsPageAsync(int skip, int take)
        {
            var totalRecords = await context.Product.CountAsync();
            var products = await context.Product
                                 .OrderBy(p => p.Name)
                                 .Skip(skip)
                                 .Take(take)
                                 .ToListAsync();
            return new PagingResult<Product>(products, totalRecords);
        }

        public async Task<Product> InsertProductAsync(Product product)
        {
            context.Product.Add(product);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception exp)
            {
                logger.LogError($"Error in {nameof(InsertProductAsync)}: " + exp.Message);
            }

            return product;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            context.Product.Attach(product);
            context.Entry(product).State = EntityState.Modified;
            try
            {
                return (await context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception exp)
            {
                logger.LogError($"Error in {nameof(UpdateProductAsync)}: " + exp.Message);
            }
            return false;
        }
    }
}
