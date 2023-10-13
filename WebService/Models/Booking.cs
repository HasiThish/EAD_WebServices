using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebService.Models
{
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("bookingId")]
        public string BookingId { get; set; }

        [BsonElement("travelnic")]
        public string TravelNIC { get; set; }

        [BsonElement("trainno")]
        public string TrainNumber { get; set; }

        [BsonElement("seatcount")]
        public int SeatCount { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }
    }

}
