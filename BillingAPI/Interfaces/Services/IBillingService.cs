using BillingAPI.Common;

namespace BillingAPI.Interfaces.Services
{
    public interface IBillingService
    {
        public Task<Response> CreateBillingsAsync();
    }
}
