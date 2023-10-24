using Microsoft.AspNetCore.Mvc;
using WebService.Models;
using WebService.Services;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainsController : ControllerBase
    {
        private readonly ITrainService _trainService;

        public TrainsController(ITrainService trainService)
        {
            // Initialize the controller with an instance of the ITrainService.
            _trainService = trainService;
        }

        // Retrieve all trains.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Train>>> GetAllTrainsAsync()
        {
            try
            {
                var trains = await _trainService.GetAllTrainsAsync();
                return Ok(trains);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an appropriate response.
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Retrieve a train by its unique ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<Train>> GetTrainByIdAsync(string id)
        {
            try
            {
                var train = await _trainService.GetTrainByIdAsync(id);
                if (train == null)
                {
                    return NotFound(); // Return a 404 Not Found status if the train is not found.
                }
                return Ok(train);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an appropriate response.
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Add a new train.
        [HttpPost]
        public async Task<ActionResult<string>> AddTrainAsync([FromBody] Train train)
        {
            try
            {
                var trainId = await _trainService.AddTrainAsync(train);
                return trainId;
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an appropriate response.
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Update an existing train by its ID.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrainAsync(string id, [FromBody] Train train)
        {
            try
            {
                var result = await _trainService.UpdateTrainAsync(id, train);
                if (result)
                {
                    return NoContent(); // Return a 204 No Content status if the update is successful.
                }
                return NotFound(); // Return a 404 Not Found status if the train is not found.
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an appropriate response.
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Delete a train by its ID.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainAsync(string id)
        {
            try
            {
                var result = await _trainService.DeleteTrainAsync(id);
                if (result)
                {
                    return NoContent(); // Return a 204 No Content status if the deletion is successful.
                }
                return NotFound(); // Return a 404 Not Found status if the train is not found.
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an appropriate response.
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Retrieve train schedules for a specific train by its ID.
        [HttpGet("{id}/schedules")]
        public async Task<ActionResult<TrainSchedule>> GetTrainSchedules(string id)
        {
            var schedules = await _trainService.GetTrainSchedulesByTrainIdAsync(id);
            if (schedules == null)
            {
                return NotFound(); // Return a 404 Not Found response if the train with the given ID is not found.
            }
            return Ok(schedules);
        }

        // Add a train schedule to a specific train by its ID.
        [HttpPost("{id}/schedules")]
        public async Task<ActionResult> AddTrainSchedule(string id, [FromBody] TrainSchedule schedule)
        {
            var success = await _trainService.AddTrainScheduleAsync(id, schedule);
            if (success)
            {
                return Ok(); // Return a 200 OK response if the schedule was added successfully.
            }
            return NotFound(); // Return a 404 Not Found response if the train with the given ID is not found.
        }

        // Update a train schedule within a specific train by its ID and schedule ID.
        [HttpPut("{trainId}/schedules/{scheduleId}")]
        public async Task<IActionResult> UpdateTrainSchedule(string trainId, string scheduleId, [FromBody] TrainSchedule updatedSchedule)
        {
            bool success = await _trainService.UpdateTrainScheduleAsync(trainId, scheduleId, updatedSchedule);
            if (success)
            {
                return Ok();
            }
            return NotFound(); // Handle errors as needed.
        }

        // Delete a train schedule within a specific train by its ID and schedule ID.
        [HttpDelete("{trainId}/schedules/{scheduleId}")]
        public async Task<IActionResult> DeleteTrainSchedule(string trainId, string scheduleId)
        {
            bool success = await _trainService.DeleteTrainScheduleAsync(trainId, scheduleId);
            if (success)
            {
                return NoContent();
            }
            return NotFound(); // Handle errors as needed.
        }

        [HttpPost("searchedtrains")]
        public async Task<ActionResult<List<Train>>> GetTrainsBetweenStationsAsync([FromBody] StationsRequest request)
        {
            var filteredTrains = await _trainService.GetTrainsByStationsAsync(request.StartStation, request.EndStation);

            if (filteredTrains == null)
            {
                return NotFound("No trains found for the given stations.");
            }
            else
            {
                return filteredTrains;
            }
        }
    }
}
