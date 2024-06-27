using Newtonsoft.Json;

namespace BillingAPI.DTO
{
    public class BillingLineDto
    {
        [JsonProperty("productId")]
        public string ProductId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("unit_price")]
        public decimal UnitPrice { get; set; }

        [JsonProperty("subtotal")]
        public decimal Subtotal { get; set; }
    }
}
