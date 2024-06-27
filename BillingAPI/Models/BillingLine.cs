using MongoDB.Bson.Serialization.Attributes;

namespace BillingAPI.Models
{
    public class BillingLine : BaseModel
    {
        public BillingLine(string id,string productId, string billingId)
        {
            Id = id;
            ProductId = productId;
            BillingId = billingId;
            IsDeleted = false;
        }

        [BsonElement("billingId")]
        public string BillingId { get; set; }

        [BsonElement("productId")]
        public string ProductId { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("unit_price")]
        public decimal UnitPrice { get; set; }

        [BsonElement("subtotal")]
        public decimal Subtotal { get; set; }
    }
}
