namespace BillingAPI.Models
{
    public class BillingLine : BaseModel
    {
        public BillingLine(string id, string productId, string billingId)
        {
            Id = id;
            ProductId = productId;
            BillingId = billingId;
            IsDeleted = false;
        }

        public string BillingId { get; set; }

        public string ProductId { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Subtotal { get; set; }
    }
}
