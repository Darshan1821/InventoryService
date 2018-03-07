using InventoryService.Model;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }

        public DbSet<Product> Product { get; set; }
    }
}
