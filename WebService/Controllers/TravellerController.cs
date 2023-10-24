using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebService.Models;
using WebService.Services;

namespace WebService.Controllers
{
    [Route("api/travelers")]
    [ApiController]
    public class TravelerController : ControllerBase
    {
        private readonly ITravellerService _travelerService;

        public TravelerController(ITravellerService travelerService)
        {
            // Initialize the controller with an instance of the ITravellerService.
            _travelerService = travelerService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            // Validate user credentials against MongoDB
            var user = await _travelerService.GetUserByUsernameAsync(loginModel.Username, loginModel.Password);

            return Ok(user);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok("Logout successful");
        }

        // Retrieve all travelers.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Traveller>>> GetAllTravelers()
        {
            var travelers = await _travelerService.GetAllTravelersAsync();
            return Ok(travelers);
        }

        // Retrieve a traveler by their unique ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<Traveller>> GetTravelerById(string id)
        {
            var traveler = await _travelerService.GetTravelerByIdAsync(id);
            if (traveler == null)
            {
                return NotFound(); // Return a 404 Not Found status if the traveler is not found.
            }
            return Ok(traveler);
        }

        // Create a new traveler.
        [HttpPost]
        public async Task<ActionResult<string>> CreateTraveler([FromBody] Traveller traveler)
        {
            var travelerId = await _travelerService.CreateTravelerAsync(traveler);
            return travelerId;
        }

        // Update an existing traveler by their ID.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTraveler(string id, [FromBody] Traveller updatedTraveler)
        {
            var success = await _travelerService.UpdateTravelerAsync(id, updatedTraveler);
            if (success)
            {
                return NoContent(); // Return a 204 No Content status if the update is successful.
            }
            return NotFound(); // Return a 404 Not Found status if the traveler is not found.
        }

        // Delete a traveler by their ID.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraveler(string id)
        {
            var success = await _travelerService.DeleteTravelerAsync(id);
            if (success)
            {
                return NoContent(); // Return a 204 No Content status if the deletion is successful.
            }
            return NotFound(); // Return a 404 Not Found status if the traveler is not found.
        }
    }
}
