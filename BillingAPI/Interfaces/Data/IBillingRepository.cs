using BillingAPI.Models;

namespace BillingAPI.Interfaces.Data
{
    public interface IBillingRepository
    {
        public Task InsertBillingAsync(Billing billing);
    }
}
