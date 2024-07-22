using BillingAPI.Interfaces.Data;
using BillingAPI.Models;
using MongoDB.Driver;

namespace BillingAPI.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly MongoDbContext _context;

        public ProductRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _context.Products.Find<Product>(product => product.Id == id && !product.IsDeleted).FirstOrDefaultAsync();
        }

        public async Task CreateProductAsync(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task UpdateProductAsync(string id, Product product)
        {
            var filter = Builders<Product>.Filter.Eq(c => c.Id, id);
            var update = Builders<Product>.Update
                .Set(c => c.ProductName, product.ProductName);

            await _context.Products.UpdateOneAsync(filter, update);
        }

        public async Task DeleteProductAsync(string id)
        {
            var filter = Builders<Product>.Filter.Eq(c => c.Id, id);
            var update = Builders<Product>.Update
                .Set(c => c.IsDeleted, true);

            await _context.Products.UpdateOneAsync(filter, update);
        }
    }
}
