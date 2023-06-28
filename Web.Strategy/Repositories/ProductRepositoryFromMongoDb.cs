using MongoDB.Driver;
using Web.Strategy.Models;

namespace Web.Strategy.Repositories
{
    public class ProductRepositoryFromMongoDb : IProductRepository
    {
        private readonly IMongoCollection<Product> _productCollection;

        public ProductRepositoryFromMongoDb(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDb");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("ProductDb");

            _productCollection = database.GetCollection<Product>("Products");
        }

        public async Task DeleteAsync(Product product)
        {
            await _productCollection.DeleteOneAsync(x=>x.Id == product.Id);
        }

        public async Task<List<Product>> GetAllByUserIdAsync(string userId)
        {
            return _productCollection.Find<Product>(userId).ToList();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _productCollection.Find<Product>(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> Save(Product product)
        {
            await _productCollection.InsertOneAsync(product);
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            await _productCollection.FindOneAndReplaceAsync(x => x.Id == product.Id, product);
        }
    }
}
