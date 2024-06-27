using BillingAPI.Models;

namespace BillingAPI.Adapters.BillingAdapter
{
    public class BillingAdapterResponse
    {
        public BillingAdapterResponse(List<Billing> billingsToCreate, List<BillingLine> billingLinesToCreate)
        {
            BillingsToCreate = billingsToCreate;
            BillingLinesToCreate = billingLinesToCreate;
        }

        public List<Billing> BillingsToCreate { get; set; }
        public List<BillingLine> BillingLinesToCreate { get; set; }
    }
}
