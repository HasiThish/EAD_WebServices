using WebService.Models;

namespace WebService.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(string id);
        Task<string> AddEmployeeAsync(Employee employee);
        Task<bool> UpdateEmployeeAsync(string id, Employee employee);
        Task<bool> DeleteEmployeeAsync(string id);
        Task<Employee> GetUserByUsernameAsync(string username, string password);
    }
}
