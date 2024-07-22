using BillingAPI.Interfaces.Data;
using BillingAPI.Models;

namespace BillingAPI.Data
{
    public class BillingRepository : IBillingRepository
    {
        private readonly MongoDbContext _context;

        public BillingRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task InsertBillingAsync(Billing billing)
        {
            await _context.Billings.InsertOneAsync(billing);
        }
    }
}
