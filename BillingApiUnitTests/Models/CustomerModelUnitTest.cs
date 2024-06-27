using BillingAPI.Models;

namespace BillingApiUnitTests.Models
{
    public class CustomerModelUnitTest
    {
        [Fact]
        public void Customer_ShouldBeInitializedWithValidParameters()
        {
            var id = "testeid";
            var name = "nameteste";
            var email = "customer@teste.com";
            var address = "Rua Teste 43";

            var customer = new Customer(id, name, email, address);

            Assert.Equal(id, customer.Id);
            Assert.Equal(name, customer.Name);
            Assert.Equal(email, customer.Email);
            Assert.Equal(address, customer.Address);
            Assert.False(customer.IsDeleted);
        }
    }
}
