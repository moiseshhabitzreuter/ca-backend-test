using BillingAPI.Models;

namespace BillingApiUnitTests.Models
{
    public class BillingModelUnitTest
    {
        [Fact]
        public void Billing_ShouldBeInitializedWithValidParameters()
        {
            var id = "testeid";
            var invoiceNumber = "invoiceteste";

            var billing = new Billing(id, invoiceNumber);

            Assert.Equal(id, billing.Id);
            Assert.Equal(invoiceNumber, billing.InvoiceNumber);
            Assert.False(billing.IsDeleted);
        }

        [Fact]
        public void Billing_ShouldSetAndGetProperties()
        {
            var id = "testeid";
            var invoiceNumber = "invoiceteste";
            var billing = new Billing(id, invoiceNumber);

            var customer = new Customer("customeridteste", "customerteste", "customer1@teste.com", "Rua Teste 13");
            var date = new DateTime(2024, 2, 1);
            var dueDate = new DateTime(2024, 2, 8);
            var totalAmount = 100m;
            var currency = "BRL";

            billing.Customer = customer;
            billing.Date = date;
            billing.DueDate = dueDate;
            billing.TotalAmount = totalAmount;
            billing.Currency = currency;

            Assert.Equal(customer, billing.Customer);
            Assert.Equal(date, billing.Date);
            Assert.Equal(dueDate, billing.DueDate);
            Assert.Equal(totalAmount, billing.TotalAmount);
            Assert.Equal(currency, billing.Currency);
        }
    }
}
