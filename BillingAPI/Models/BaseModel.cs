using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BillingAPI.Models
{
    public class BaseModel
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}
