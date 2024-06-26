using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BillingAPI.Models
{
    public class Customer
    {
        public Customer(string id, string name, string email, string address)
        {
            Id = id;
            Name = name;
            Email = email;
            Address = address;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Address")]
        public string Address { get; set; }
    }
}
