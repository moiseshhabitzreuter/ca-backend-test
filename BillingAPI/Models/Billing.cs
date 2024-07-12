namespace BillingAPI.Models
{
    public class Billing : BaseModel
    {
        public Billing(string id, string invoiceNumber)
        {
            Id = id;
            InvoiceNumber = invoiceNumber;
            IsDeleted = false;
        }

        public string InvoiceNumber { get; set; }

        public Customer Customer { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string Currency { get; set; }

        public string CustomerId { get; set; }
    }
}
