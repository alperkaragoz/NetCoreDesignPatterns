using Microsoft.EntityFrameworkCore;
using Web.Strategy.Models;

namespace Web.Strategy.Repositories
{
    public class ProductRepositoryFromSqlServer : IProductRepository
    {
        private readonly AppIdentityDbContext _context;

        public ProductRepositoryFromSqlServer(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(Product product)
        {
            // Remove'un async olmamasının sebebi; async bir işlem yok, memory'deki product classının State'i Deleted olarak işaretleniyor.
            _context.Products.Remove(product);
            // Aşağıdaki kod ile üstteki kod arasında bir fark yoktur.
            //_context.Entry(product).State=Microsoft.EntityFrameworkCore.EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllByUserIdAsync(string userId)
        {
            return await _context.Products.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task<Product> Save(Product product)
        {
            product.Id = Guid.NewGuid().ToString();
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
