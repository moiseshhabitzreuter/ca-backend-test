using BillingAPI.Models;

namespace BillingApiUnitTests.Models
{
    public class CustomerModelUnitTest
    {
        [Fact]
        public void Customer_Name_Should_Set_And_Get_Correctly()
        {
            var customer = new Customer("testId", "testName", "testEmail", "testAddress");
            var expectedName = "Johnny Test";

            customer.Name = expectedName;

            Assert.Equal(expectedName, customer.Name);
        }

        [Fact]
        public void Customer_Email_Should_Set_And_Get_Correctly()
        {
            var customer = new Customer("testId", "testName", "testEmail", "testAddress");
            var expectedEmail = "test@example.com";

            customer.Email = expectedEmail;

            Assert.Equal(expectedEmail, customer.Email);
        }

        [Fact]
        public void Customer_Address_Should_Set_And_Get_Correctly()
        {
            var customer = new Customer("testId", "testName", "testEmail", "testAddress");
            var expectedAddress = "Rua Teste 21";

            customer.Address = expectedAddress;

            Assert.Equal(expectedAddress, customer.Address);
        }
    }
}
