using MongoDB.Driver;
using WebService.Models;

namespace WebService.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMongoCollection<Employee> _employeeCollection;

        public EmployeeService(IMongoDBSettings settings, IMongoClient mongoClient)
        {
            // Initialize the MongoDB collection for employees.
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _employeeCollection = database.GetCollection<Employee>(settings.EmployeeCollection);
        }

        // Retrieve all employees from the MongoDB collection.
        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _employeeCollection.Find(_ => true).ToListAsync();
        }

        // Retrieve an employee by their unique ID.
        public async Task<Employee> GetEmployeeByIdAsync(string id)
        {
            return await _employeeCollection.Find(employee => employee.Id == id).FirstOrDefaultAsync();
        }

        // Add a new employee to the collection, ensuring the username is unique.
        public async Task<string> AddEmployeeAsync(Employee employee)
        {
            // Check if an employee with the same username already exists.
            var existuser = await _employeeCollection.Find(em => em.Username == employee.Username).FirstOrDefaultAsync();

            if (existuser == null)
            {
                // Insert the new employee into the MongoDB collection.
                await _employeeCollection.InsertOneAsync(employee);
                return employee.Id; // Return the ID of the newly added employee.
            }
            else
            {
                // Return null to indicate that an employee with the same username already exists.
                return null;
            }
        }

        // Update an existing employee by their ID.
        public async Task<bool> UpdateEmployeeAsync(string id, Employee employee)
        {
            // Replace the existing employee document with the updated one.
            var updateResult = await _employeeCollection.ReplaceOneAsync(e => e.Id == id, employee);
            return updateResult.ModifiedCount > 0; // Return true if the update was successful.
        }

        // Delete an employee by their ID.
        public async Task<bool> DeleteEmployeeAsync(string id)
        {
            // Delete the employee document with the specified ID.
            var deleteResult = await _employeeCollection.DeleteOneAsync(employee => employee.Id == id);
            return deleteResult.DeletedCount > 0; // Return true if a document was deleted.
        }

        // Authenticate a user by their username and password.
        public async Task<Employee> GetUserByUsernameAsync(string username, string password)
        {
            try
            {
                // Find and return the user with the given username and password.
                var user = await _employeeCollection.Find(employee => employee.Username == username && employee.Password == password).FirstOrDefaultAsync();
                return user;
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., MongoDB connection error, etc.) by rethrowing the exception.
                throw ex;
            }
        }
    }
}
