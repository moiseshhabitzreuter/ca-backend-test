using BillingAPI.Controllers;
using BillingAPI.Interfaces.Services;
using BillingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingApiUnitTests.Controllers
{
    public class ProductControllerUnitTest
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductController _controller;

        public ProductControllerUnitTest()
        {
            _mockProductService = new Mock<IProductService>();
            _controller = new ProductController(_mockProductService.Object);
        }

        [Fact]
        public async Task GetProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            _mockProductService.Setup(service => service.GetProductByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((Product)null);

            var result = await _controller.GetProduct("nonexistantid");

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetProduct_ReturnsOk_WhenProductExists()
        {
            var product = CreateTestProduct();
            _mockProductService.Setup(service => service.GetProductByIdAsync("1"))
                .ReturnsAsync(product);

            var result = await _controller.GetProduct("1");

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<Product>(okResult.Value);
            Assert.Equal("ProductName", returnedProduct.ProductName);
        }

        [Fact]
        public async Task CreateProduct_ReturnsCreatedAtRoute()
        {
            var product = CreateTestProduct();
            _mockProductService.Setup(service => service.CreateProductAsync(product))
                .Returns(Task.CompletedTask);

            var result = await _controller.CreateProduct(product);

            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal("GetProduct", createdAtRouteResult.RouteName);
            Assert.Equal("1", createdAtRouteResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            _mockProductService.Setup(service => service.GetProductByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((Product)null);

            var result = await _controller.UpdateProduct("nonexistantid", CreateTestProduct());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsNoContent_WhenProductIsUpdated()
        {
            var existingProduct = CreateTestProduct();
            _mockProductService.Setup(service => service.GetProductByIdAsync("1"))
                .ReturnsAsync(existingProduct);
            _mockProductService.Setup(service => service.UpdateProductAsync("1", existingProduct))
                .Returns(Task.CompletedTask);

            var result = await _controller.UpdateProduct("1", existingProduct);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            _mockProductService.Setup(service => service.GetProductByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((Product)null);

            var result = await _controller.DeleteProduct("nonexistantid");

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNoContent_WhenProductIsDeleted()
        {
            var existingProduct = CreateTestProduct();
            _mockProductService.Setup(service => service.GetProductByIdAsync("1"))
                .ReturnsAsync(existingProduct);
            _mockProductService.Setup(service => service.DeleteProductAsync("1"))
                .Returns(Task.CompletedTask);

            var result = await _controller.DeleteProduct("1");

            Assert.IsType<NoContentResult>(result);
        }

        private Product CreateTestProduct()
        {
            var product = new Product("1", "ProductName");
            return product;
        }
    }
}
