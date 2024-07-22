using BillingAPI.Interfaces.Data;
using BillingAPI.Models;

namespace BillingAPI.Data
{
    public class BillingLinesRepository : IBillingLinesRepository
    {
        private readonly MongoDbContext _context;

        public BillingLinesRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task InsertBillingLinesAsync(List<BillingLine> billingLines)
        {
            await _context.BillingLines.InsertManyAsync(billingLines);
        }
    }
}
