using InventoryService.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryService.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync();
        Task<PagingResult<Product>> GetProductsPageAsync(int skip, int take);
        Task<Product> GetProductAsync(int id);

        Task<Product> InsertProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}
