using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BillingAPI.Models
{
    public class BaseModel
    {

        [BsonElement("Id")]
        public string Id { get; set; }

        [BsonElement("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}
