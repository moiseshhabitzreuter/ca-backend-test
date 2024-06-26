using BillingAPI.Models;

namespace BillingApiUnitTests.Models
{
    public class ProductModelUnitTest
    {
        [Fact]
        public void ProductId_Should_Set_And_Get_Correctly()
        {
            var product = new Product();
            var expectedId = "IdTest";

            product.Id = expectedId;

            Assert.Equal(expectedId, product.Id);
        }

        [Fact]
        public void ProductName_Should_Set_And_Get_Correctly()
        {
            var product = new Product();
            var expectedProductName = "ProductTest";

            product.ProductName = expectedProductName;

            Assert.Equal(expectedProductName, product.ProductName);
        }
    }
}
