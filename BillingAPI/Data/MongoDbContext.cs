using BillingAPI.Models;
using MongoDB.Driver;

namespace BillingAPI.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
            _database = client.GetDatabase(configuration.GetSection("ConnectionStrings:DatabaseName").Value);
        }

        public IMongoCollection<Customer> Customers => _database.GetCollection<Customer>("customers");
        public IMongoCollection<Product> Products => _database.GetCollection<Product>("products");
    }
}
