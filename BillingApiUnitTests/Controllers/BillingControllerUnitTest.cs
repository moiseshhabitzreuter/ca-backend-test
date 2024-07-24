using BillingAPI.Common;
using BillingAPI.Controllers;
using BillingAPI.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;

namespace BillingApiUnitTests.Controllers
{
    public class BillingControllerUnitTest
    {
        private readonly Mock<IBillingService> _mockBillingService;
        private readonly BillingController _controller;

        public BillingControllerUnitTest()
        {
            _mockBillingService = new Mock<IBillingService>();
            _controller = new BillingController(_mockBillingService.Object);
        }

        [Fact]
        public async Task CreateBillings_ReturnsOk_WhenBillingIsCreated()
        {
            var response = new Response();
            _mockBillingService.Setup(service => service.CreateBillingsAsync())
                .ReturnsAsync(response);

            var result = await _controller.CreateBillings();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        [Fact]
        public async Task CreateBillings_ReturnsServiceUnavailable_WhenHttpRequestExceptionIsThrown()
        {
            _mockBillingService.Setup(service => service.CreateBillingsAsync())
                .ThrowsAsync(new HttpRequestException());

            var result = await _controller.CreateBillings();

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(503, statusCodeResult.StatusCode);
            Assert.Equal("Service Unavailable: Unable to reach the external API.", statusCodeResult.Value);
        }

        [Fact]
        public async Task CreateBillings_ReturnsInternalServerError_WhenJsonExceptionIsThrown()
        {
            _mockBillingService.Setup(service => service.CreateBillingsAsync())
                .ThrowsAsync(new JsonException());

            var result = await _controller.CreateBillings();

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Internal Server Error: Invalid JSON response from the external API.", statusCodeResult.Value);
        }

        [Fact]
        public async Task CreateBillings_ReturnsInternalServerError_WhenGeneralExceptionIsThrown()
        {
            var errorMessage = "errorMessage";
            _mockBillingService.Setup(service => service.CreateBillingsAsync())
                .ThrowsAsync(new System.Exception(errorMessage));

            var result = await _controller.CreateBillings();

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal($"Internal Server Error: {errorMessage}", statusCodeResult.Value);
        }
    }
}
