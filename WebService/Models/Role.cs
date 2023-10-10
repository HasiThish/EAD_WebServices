using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebService.Models
{
    public class Role
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("role")]
        public string RoleName { get; set; }

        [BsonElement("menu")]
        public List<string> MenuItems { get; set; }
    }
}
