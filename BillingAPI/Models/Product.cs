using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BillingAPI.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("ProductName")]
        public string ProductName { get; set; }
    }
}
