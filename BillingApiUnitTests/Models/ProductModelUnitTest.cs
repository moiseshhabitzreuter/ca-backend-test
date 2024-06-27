using BillingAPI.Models;

namespace BillingApiUnitTests.Models
{
    public class ProductModelUnitTest
    {
        [Fact]
        public void Product_ShouldBeInitializedWithValidParameters()
        {
            var id = "testeid";
            var productName = "productnameteste";

            var product = new Product(id, productName);

            Assert.Equal(id, product.Id);
            Assert.Equal(productName, product.ProductName);
            Assert.False(product.IsDeleted);
        }
    }
}
