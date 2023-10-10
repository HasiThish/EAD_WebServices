using MongoDB.Driver;
using WebService.Models;

namespace WebService.Services
{
    public class RoleService : IRoleService
    {
        private readonly IMongoCollection<Role> _roleCollection;

        public RoleService(IMongoDBSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _roleCollection = database.GetCollection<Role>(settings.RoleCollection);
        }
       
        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _roleCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Role> GetRoleByIdAsync(string id)
        {
            return await _roleCollection.Find(role => role.Id == id).FirstOrDefaultAsync();
        }

        public async Task<string> AddRoleAsync(Role role)
        {
            await _roleCollection.InsertOneAsync(role);
            return role.Id;
        }

        public async Task<bool> UpdateRoleAsync(string id, Role role)
        {
            var updateResult = await _roleCollection.ReplaceOneAsync(r => r.Id == id, role);
            return updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteRoleAsync(string id)
        {
            var deleteResult = await _roleCollection.DeleteOneAsync(role => role.Id == id);
            return deleteResult.DeletedCount > 0;
        }
    }
}
