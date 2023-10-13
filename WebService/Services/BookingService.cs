using MongoDB.Driver;
using WebService.Models;

namespace WebService.Services
{
    public class BookingService:IBookingService
    {
        private readonly IMongoCollection<Booking> _bookingCollection;
        private readonly IMongoCollection<Train> _trainCollection;
        private readonly IMongoCollection<Traveller> _travelerCollection;

        public BookingService(IMongoDBSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _bookingCollection = database.GetCollection<Booking>(settings.BookingCollection);
            _trainCollection = database.GetCollection<Train>(settings.TrainCollection);
            _travelerCollection = database.GetCollection<Traveller>(settings.TravellerCollection);
        }

        //get all bookings
        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _bookingCollection.Find(_ => true).ToListAsync();
        }

        //get booking by id
        public async Task<Booking> GetBookingByIdAsync(string id)
        {
            return await _bookingCollection.Find(booking => booking.Id == id).FirstOrDefaultAsync();
        }

        //create booking
        public async Task<string> CreateBookingAsync(Booking booking)
        {
            await _bookingCollection.InsertOneAsync(booking);
            return booking.Id;
        }

        //update booking
        public async Task<bool> UpdateBookingAsync(string id, Booking updatedBooking)
        {
            var updateResult = await _bookingCollection.ReplaceOneAsync(b => b.Id == id, updatedBooking);
            return updateResult.ModifiedCount > 0;
        }

        //delete booking
        public async Task<bool> DeleteBookingAsync(string id)
        {
            var deleteResult = await _bookingCollection.DeleteOneAsync(booking => booking.Id == id);
            return deleteResult.DeletedCount > 0;
        }

        //get bookings by traveller's nic
        public async Task<IEnumerable<Booking>> GetBookingsByTravelerNICAsync(string travelerNIC)
        {
            var filter = Builders<Booking>.Filter.Eq(b => b.TravelNIC, travelerNIC);
            var bookings = await _bookingCollection.Find(filter).ToListAsync();
            return bookings;
        }

        //get train by train number
        public async Task<Train> GetTrainByTrainNumberAsync(string trainNumber)
        {
            var filter = Builders<Train>.Filter.Eq(t => t.TrainNumber, trainNumber);
            var train = await _trainCollection.Find(filter).FirstOrDefaultAsync();
            return train;
        }

        //get traveller details using nic
        public async Task<Traveller> GetTravelerByNICAsync(string travelerNIC)
        {
            var filter = Builders<Traveller>.Filter.Eq(t => t.NIC, travelerNIC);
            var traveler = await _travelerCollection.Find(filter).FirstOrDefaultAsync();
            return traveler;
        }

        //get booking details with train and traveller details using nic
        public async Task<TrainBookingDetails> GetTrainBookingDetailsByTravelerNICAsync(string travelerNIC)
        {
            var bookings = await GetBookingsByTravelerNICAsync(travelerNIC);
            var traveler = await GetTravelerByNICAsync(travelerNIC);

            if (traveler == null)
            {
                // Handle the case when the traveler is not found.
                return null; 
            }
            
            var trainBookingDetails = new TrainBookingDetails();
            trainBookingDetails.Traveller = traveler;
            trainBookingDetails.BookingList = new List<BookedTrain>();

            foreach (var booking in bookings)
            {
                var train = await GetTrainByTrainNumberAsync(booking.TrainNumber);

                if (train != null)
                {
                    var bookedInfo = new BookedTrain
                    {
                        Booking = booking,
                        Train = train
                    };

                    trainBookingDetails.BookingList.Add(bookedInfo);
                }
            }

            return trainBookingDetails;
        }


    }
}
