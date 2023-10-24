using WebService.Models;

namespace WebService.Services
{
    public interface ITravellerService
    {
        Task<IEnumerable<Traveller>> GetAllTravelersAsync();
        Task<Traveller> GetTravelerByIdAsync(string id);
        Task<string> CreateTravelerAsync(Traveller traveler);
        Task<bool> UpdateTravelerAsync(string id, Traveller updatedTraveler);
        Task<bool> DeleteTravelerAsync(string id);
        Task<Traveller> GetUserByUsernameAsync(string username, string password);
    }
}
