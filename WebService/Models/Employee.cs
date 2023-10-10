using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebService.Models
{
    public class Employee
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("dob")]
        public DateTime Dob { get; set; }

        [BsonElement("nic")]
        public string NIC { get; set; }

        [BsonElement("mobile")]
        public string[] Mobile { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("address")]
        public Address Address { get; set; }

        [BsonElement("role")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Role { get; set; }
    }

    public class Address
    {
        [BsonElement("no")]
        public string No { get; set; }

        [BsonElement("street1")]
        public string Street1 { get; set; }

        [BsonElement("street2")]
        public string Street2 { get; set; }

        [BsonElement("city")]
        public string City { get; set; }
    }
}
