using BillingAPI.Data;
using BillingAPI.Models;

namespace BillingAPI.Interfaces.Data
{
    public interface IBillingLinesRepository
    {
        public Task InsertBillingLinesAsync(List<BillingLine> billingLines);
    }
}
