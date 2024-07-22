using BillingAPI.Models;

namespace BillingAPI.Interfaces.Services
{
    public interface ICustomerService
    {
        public Task<Customer> GetCustomerByIdAsync(string id);

        public Task CreateCustomerAsync(Customer customer);

        public Task UpdateCustomerAsync(string id, Customer customer);

        public Task DeleteCustomerAsync(string id);
    }
}
