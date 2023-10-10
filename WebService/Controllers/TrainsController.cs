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
            _trainService = trainService;
        }

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

        [HttpGet("{id}")]
        public async Task<ActionResult<Train>> GetTrainByIdAsync(string id)
        {
            try
            {
                var train = await _trainService.GetTrainByIdAsync(id);
                if (train == null)
                {
                    return NotFound();
                }
                return Ok(train);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an appropriate response.
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddTrainAsync([FromBody] Train train)
        {
            try
            {
                var trainId = await _trainService.AddTrainAsync(train);
                return CreatedAtAction(nameof(GetTrainByIdAsync), new { id = trainId }, trainId);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an appropriate response.
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrainAsync(string id, [FromBody] Train train)
        {
            try
            {
                var result = await _trainService.UpdateTrainAsync(id, train);
                if (result)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an appropriate response.
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainAsync(string id)
        {
            try
            {
                var result = await _trainService.DeleteTrainAsync(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an appropriate response.
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
