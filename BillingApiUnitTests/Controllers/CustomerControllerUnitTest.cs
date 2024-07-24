using BillingAPI.Controllers;
using BillingAPI.Interfaces.Services;
using BillingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BillingApiUnitTests.Controllers
{
    public class CustomerControllerUnitTest
    {
        private readonly Mock<ICustomerService> _mockCustomerService;
        private readonly CustomerController _controller;

        public CustomerControllerUnitTest()
        {
            _mockCustomerService = new Mock<ICustomerService>();
            _controller = new CustomerController(_mockCustomerService.Object);
        }

        [Fact]
        public async Task GetCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            _mockCustomerService.Setup(service => service.GetCustomerByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((Customer)null);

            var result = await _controller.GetCustomer("nonexistantid");

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetCustomer_ReturnsOk_WhenCustomerExists()
        {
            var customer = CreateTestCustomer();
            _mockCustomerService.Setup(service => service.GetCustomerByIdAsync("1"))
                .ReturnsAsync(customer);

            var result = await _controller.GetCustomer("1");

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCustomer = Assert.IsType<Customer>(okResult.Value);
            Assert.Equal("CustomerTestName", returnedCustomer.Name);
        }

        [Fact]
        public async Task CreateCustomer_ReturnsCreatedAtRoute()
        {
            var customer = CreateTestCustomer();
            _mockCustomerService.Setup(service => service.CreateCustomerAsync(customer))
                .Returns(Task.CompletedTask);

            var result = await _controller.CreateCustomer(customer);

            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal("GetCustomer", createdAtRouteResult.RouteName);
            Assert.Equal("1", createdAtRouteResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdateCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            _mockCustomerService.Setup(service => service.GetCustomerByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((Customer)null);

            var result = await _controller.UpdateCustomer("nonexistantid", CreateTestCustomer());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateCustomer_ReturnsNoContent_WhenCustomerIsUpdated()
        {
            var existingCustomer = CreateTestCustomer();
            _mockCustomerService.Setup(service => service.GetCustomerByIdAsync("1"))
                .ReturnsAsync(existingCustomer);
            _mockCustomerService.Setup(service => service.UpdateCustomerAsync("1", existingCustomer))
                .Returns(Task.CompletedTask);

            var result = await _controller.UpdateCustomer("1", existingCustomer);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            _mockCustomerService.Setup(service => service.GetCustomerByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((Customer)null);

            var result = await _controller.DeleteCustomer("nonexistantid");

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteCustomer_ReturnsNoContent_WhenCustomerIsDeleted()
        {
            var existingCustomer = CreateTestCustomer();
            _mockCustomerService.Setup(service => service.GetCustomerByIdAsync("1"))
                .ReturnsAsync(existingCustomer);
            _mockCustomerService.Setup(service => service.DeleteCustomerAsync("1"))
                .Returns(Task.CompletedTask);

            var result = await _controller.DeleteCustomer("1");

            Assert.IsType<NoContentResult>(result);
        }

        private Customer CreateTestCustomer()
        {
            var customer = new Customer("1", "CustomerTestName", "test@email.com", "Test Address 01");
            return customer;
        }
    }
}
