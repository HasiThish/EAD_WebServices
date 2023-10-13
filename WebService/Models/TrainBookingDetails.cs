namespace WebService.Models
{
    public class TrainBookingDetails
    {
        public Traveller Traveller { get; set; } // Traveler details

        public List<BookedTrain> BookingList {  get; set; }

    }

    public class BookedTrain
    {
        public Train Train { get; set; } // Train details

        public Booking Booking { get; set; } // Booking details
    }

}
