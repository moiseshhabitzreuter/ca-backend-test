using BillingAPI.Models;

namespace BillingAPI.Interfaces
{
    public interface IProductService
    {
        public Task<Product> GetProductByIdAsync(string id);

        public Task CreateProductAsync(Product product);

        public Task UpdateProductAsync(string id, Product product);

        public Task DeleteProductAsync(string id);
    }
}
