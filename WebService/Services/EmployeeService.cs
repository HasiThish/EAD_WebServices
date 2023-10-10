using MongoDB.Driver;
using WebService.Models;

namespace WebService.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMongoCollection<Employee> _employeeCollection;

        public EmployeeService(IMongoDBSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _employeeCollection = database.GetCollection<Employee>(settings.EmployeeCollection);
        }
        
        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _employeeCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(string id)
        {
            return await _employeeCollection.Find(employee => employee.Id == id).FirstOrDefaultAsync();
        }

        public async Task<string> AddEmployeeAsync(Employee employee)
        {
            var existuser = await _employeeCollection.Find(em => em.Username == employee.Username).FirstOrDefaultAsync();

            if (existuser == null)
            {
                await _employeeCollection.InsertOneAsync(employee);
                return employee.Id;
            }
            else
            {
                return null;
            }
            
            
        }

        public async Task<bool> UpdateEmployeeAsync(string id, Employee employee)
        {
            var updateResult = await _employeeCollection.ReplaceOneAsync(e => e.Id == id, employee);
            return updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteEmployeeAsync(string id)
        {
            var deleteResult = await _employeeCollection.DeleteOneAsync(employee => employee.Id == id);
            return deleteResult.DeletedCount > 0;
        }

        public async Task<Employee> GetUserByUsernameAsync(string username, string password)
        {
            try
            {
                var user = await _employeeCollection.Find(employee => employee.Username == username && employee.Password == password).FirstOrDefaultAsync();
                return user;
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., MongoDB connection error, etc.) here
                throw ex;
            }
        }
    }
}
