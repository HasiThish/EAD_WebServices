using MongoDB.Driver;
using WebService.Models;

namespace WebService.Services
{
    public class RoleService : IRoleService
    {
        private readonly IMongoCollection<Role> _roleCollection;

        public RoleService(IMongoDBSettings settings, IMongoClient mongoClient)
        {
            // Initialize the MongoDB collection for roles using the provided settings.
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _roleCollection = database.GetCollection<Role>(settings.RoleCollection);
        }

        // Retrieve all roles from the MongoDB collection.
        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _roleCollection.Find(_ => true).ToListAsync();
        }

        // Retrieve a role by its unique ID.
        public async Task<Role> GetRoleByIdAsync(string id)
        {
            return await _roleCollection.Find(role => role.Id == id).FirstOrDefaultAsync();
        }

        // Add a new role to the collection.
        public async Task<string> AddRoleAsync(Role role)
        {
            // Insert the new role into the MongoDB collection and return its ID.
            await _roleCollection.InsertOneAsync(role);
            return role.Id;
        }

        // Update an existing role by its ID.
        public async Task<bool> UpdateRoleAsync(string id, Role role)
        {
            // Replace the existing role document with the updated one.
            var updateResult = await _roleCollection.ReplaceOneAsync(r => r.Id == id, role);
            return updateResult.ModifiedCount > 0; // Return true if the update was successful.
        }

        // Delete a role by its ID.
        public async Task<bool> DeleteRoleAsync(string id)
        {
            // Delete the role document with the specified ID.
            var deleteResult = await _roleCollection.DeleteOneAsync(role => role.Id == id);
            return deleteResult.DeletedCount > 0; // Return true if a document was deleted.
        }
    }
}
