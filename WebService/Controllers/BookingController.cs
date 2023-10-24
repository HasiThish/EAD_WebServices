using Microsoft.AspNetCore.Mvc;
using WebService.Models;
using WebService.Services;

namespace WebService.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // Retrieve all bookings.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        // Retrieve a booking by its unique ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBookingById(string id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        // Create a new booking.
        [HttpPost]
        public async Task<ActionResult<string>> CreateBooking([FromBody] Booking booking)
        {
            booking.BookingId = Booking.GenerateBookingId(_bookingService);
            var bookingId = await _bookingService.CreateBookingAsync(booking);
            return bookingId;
        }

        // Update an existing booking by its ID.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(string id, [FromBody] Booking updatedBooking)
        {
            var success = await _bookingService.UpdateBookingAsync(id, updatedBooking);
            if (success)
            {
                return NoContent();
            }
            return NotFound();
        }

        // Delete a booking by its ID.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(string id)
        {
            var success = await _bookingService.DeleteBookingAsync(id);
            if (success)
            {
                return NoContent();
            }
            return NotFound();
        }

        // Retrieve train booking details by traveler NIC.
        [HttpGet("traveller/{nic}")]
        public async Task<ActionResult<TrainBookingDetails>> GetTrainBookingDetailsByTravelerNIC(string nic)
        {
            var trainBookingDetails = await _bookingService.GetTrainBookingDetailsByTravelerNICAsync(nic);

            if (trainBookingDetails == null)
            {
                return NotFound();
            }

            return Ok(trainBookingDetails);
        }
    }
}
