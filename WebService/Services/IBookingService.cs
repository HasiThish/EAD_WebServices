using WebService.Models;

namespace WebService.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingByIdAsync(string id);
        Task<string> CreateBookingAsync(Booking booking);
        Task<bool> UpdateBookingAsync(string id, Booking updatedBooking);
        Task<bool> DeleteBookingAsync(string id);
        Task<TrainBookingDetails> GetTrainBookingDetailsByTravelerNICAsync(string travelerNIC);
    }
}
