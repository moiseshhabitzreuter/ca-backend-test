using BillingAPI.Models;

namespace BillingAPI.Interfaces.Data
{
    public interface ICustomerRepository
    {
        public Task<Customer> GetCustomerByIdAsync(string id);

        public Task CreateCustomerAsync(Customer customer);

        public Task UpdateCustomerAsync(string id, Customer customer);

        public Task DeleteCustomerAsync(string id);
    }
}
