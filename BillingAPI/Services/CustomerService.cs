using BillingAPI.Interfaces;
using BillingAPI.Models;

namespace BillingAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            await _customerRepository.CreateCustomerAsync(customer);
        }

        public async Task DeleteCustomerAsync(string id)
        {
            await _customerRepository.DeleteCustomerAsync(id);
        }

        public async Task<Customer> GetCustomerByIdAsync(string id)
        {
            return await _customerRepository.GetCustomerByIdAsync(id);
        }

        public async Task UpdateCustomerAsync(string id, Customer customer)
        {
            await _customerRepository.UpdateCustomerAsync(id, customer);
        }
    }
}
