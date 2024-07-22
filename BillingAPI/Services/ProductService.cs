using BillingAPI.Interfaces.Data;
using BillingAPI.Interfaces.Services;
using BillingAPI.Models;

namespace BillingAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task CreateProductAsync(Product product)
        {
            await _productRepository.CreateProductAsync(product);
        }

        public async Task UpdateProductAsync(string id,Product product)
        {
            await _productRepository.UpdateProductAsync(id, product);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _productRepository.DeleteProductAsync(id);
        }
    }
}
