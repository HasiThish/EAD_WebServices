using WebService.Models;

namespace WebService.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetRolesAsync();
        Task<Role> GetRoleByIdAsync(string id);
        Task<string> AddRoleAsync(Role role);
        Task<bool> UpdateRoleAsync(string id, Role role);
        Task<bool> DeleteRoleAsync(string id);
    }
}
