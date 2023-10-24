using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;
using WebService.Services;

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


        public static string GenerateBookingId(IBookingService bookingService)
        {
            int latestNumber = bookingService.GetLatestBookingNumberFromDatabase();
            string newBookingId = "OTB" + (latestNumber + 1).ToString("D4");
            return newBookingId;
        }

    }



}
