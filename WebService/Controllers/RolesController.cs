using Microsoft.AspNetCore.Mvc;
using WebService.Models;
using WebService.Services;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRolesAsync()
        {
            try
            {
                var roles = await _roleService.GetRolesAsync();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an appropriate response.
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRoleByIdAsync(string id)
        {
            try
            {
                var role = await _roleService.GetRoleByIdAsync(id);
                if (role == null)
                {
                    return NotFound();
                }
                return Ok(role);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an appropriate response.
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddRoleAsync([FromBody] Role role)
        {
            try
            {
                var roleId = await _roleService.AddRoleAsync(role);
                return CreatedAtAction(nameof(GetRoleByIdAsync), new { id = roleId }, roleId);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an appropriate response.
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoleAsync(string id, [FromBody] Role role)
        {
            try
            {
                var result = await _roleService.UpdateRoleAsync(id, role);
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
        public async Task<IActionResult> DeleteRoleAsync(string id)
        {
            try
            {
                var result = await _roleService.DeleteRoleAsync(id);
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
