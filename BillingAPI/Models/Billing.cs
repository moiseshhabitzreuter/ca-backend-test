using MongoDB.Bson.Serialization.Attributes;

namespace BillingAPI.Models
{
    public class Billing : BaseModel
    {
        public Billing(string id,string invoiceNumber)
        {
            Id = id;
            InvoiceNumber = invoiceNumber;
            IsDeleted = false;
        }

        [BsonElement("invoice_number")]
        public string InvoiceNumber { get; set; }

        [BsonElement("customer")]
        public Customer Customer { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("due_date")]
        public DateTime DueDate { get; set; }

        [BsonElement("total_amount")]
        public decimal TotalAmount { get; set; }

        [BsonElement("currency")]
        public string Currency { get; set; }
    }
}
