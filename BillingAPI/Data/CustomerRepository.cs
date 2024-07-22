using BillingAPI.Interfaces;
using BillingAPI.Models;
using MongoDB.Driver;

namespace BillingAPI.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MongoDbContext _context;

        public CustomerRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            await _context.Customers.InsertOneAsync(customer);
        }

        public async Task DeleteCustomerAsync(string id)
        {
            var filter = Builders<Customer>.Filter.Eq(c => c.Id, id);
            var update = Builders<Customer>.Update
                .Set(c => c.IsDeleted, true);

            _context.Customers.UpdateOne(filter, update);
        }

        public async Task<Customer> GetCustomerByIdAsync(string id)
        {
            return await _context.Customers.Find<Customer>(customer => customer.Id == id && !customer.IsDeleted).FirstOrDefaultAsync();
        }

        public async Task UpdateCustomerAsync(string id, Customer customer)
        {
            var filter = Builders<Customer>.Filter.Eq(c => c.Id, id);
            var update = Builders<Customer>.Update
                .Set(c => c.Name, customer.Name)
                .Set(c => c.Email, customer.Email)
                .Set(c => c.Address, customer.Address);

            await _context.Customers.UpdateOneAsync(filter, update);
        }
    }
}
