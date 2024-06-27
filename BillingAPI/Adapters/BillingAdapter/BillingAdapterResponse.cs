using BillingAPI.Models;

namespace BillingAPI.Adapters.BillingAdapter
{
    public class BillingAdapterResponse
    {
        public BillingAdapterResponse(Billing billingToCreate, List<BillingLine> billingLinesToCreate)
        {
            BillingToCreate = billingToCreate;
            BillingLinesToCreate = billingLinesToCreate;
        }

        public Billing BillingToCreate { get; set; }
        public List<BillingLine> BillingLinesToCreate { get; set; }
    }
}
