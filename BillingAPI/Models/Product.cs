using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BillingAPI.Models
{
    public class Product : BaseModel
    {
        public Product(string id,string productName)
        {
            Id = id;
            ProductName = productName;
            IsDeleted = false;
        }

        [BsonElement("ProductName")]
        public string ProductName { get; set; }
    }
}
