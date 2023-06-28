using Web.Strategy.Models;

namespace Web.Strategy.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(string id);
        Task<List<Product>> GetAllByUserIdAsync(string userId);
        Task UpdateAsync(Product product);
        Task<Product> Save(Product product);
        Task DeleteAsync(Product product);

    }
}
