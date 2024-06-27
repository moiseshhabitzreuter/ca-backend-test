using BillingAPI.Models;

namespace BillingApiUnitTests.Models
{
    public class BillingLineModelUnitTest
    {
        [Fact]
        public void BillingLine_ShouldBeInitializedWithValidParameters()
        {
            var id = "idteste";
            var productId = "productidteste";
            var billingId = "billingidteste";

            var billingLine = new BillingLine(id, productId, billingId);

            Assert.Equal(id, billingLine.Id);
            Assert.Equal(productId, billingLine.ProductId);
            Assert.Equal(billingId, billingLine.BillingId);
            Assert.False(billingLine.IsDeleted);
        }

        [Fact]
        public void BillingLine_ShouldSetAndGetProperties()
        {
            var id = "idteste";
            var productId = "productidteste";
            var billingId = "billingidteste";
            var billingLine = new BillingLine(id, productId, billingId);

            var description = "descriptionteste";
            var quantity = 5;
            var unitPrice = 20;
            var subtotal = quantity * unitPrice;

            billingLine.Description = description;
            billingLine.Quantity = quantity;
            billingLine.UnitPrice = unitPrice;
            billingLine.Subtotal = subtotal;

            Assert.Equal(description, billingLine.Description);
            Assert.Equal(quantity, billingLine.Quantity);
            Assert.Equal(unitPrice, billingLine.UnitPrice);
            Assert.Equal(subtotal, billingLine.Subtotal);
        }
    }
}
