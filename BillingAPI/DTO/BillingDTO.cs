using BillingAPI.Models;
using Newtonsoft.Json;

namespace BillingAPI.DTO
{
    public class BillingDTO
    {
        [JsonProperty("invoice_number")]
        public string InvoiceNumber { get; set; }

        [JsonProperty("customer")]
        public Customer Customer { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("due_date")]
        public DateTime DueDate { get; set; }

        [JsonProperty("total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("lines")]
        public List<BillingLineDto> Lines { get; set; }
    }
}
