using MongoDB.Driver;
using WebService.Models;

namespace WebService.Services
{
    public class TravellerService : ITravellerService
    {
        private readonly IMongoCollection<Traveller> _travelerCollection;

        public TravellerService(IMongoDBSettings settings, IMongoClient mongoClient)
        {
            // Initialize the MongoDB collection for travelers using the provided settings.
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _travelerCollection = database.GetCollection<Traveller>(settings.TravellerCollection);
        }

        // Retrieve all travelers from the MongoDB collection.
        public async Task<IEnumerable<Traveller>> GetAllTravelersAsync()
        {
            return await _travelerCollection.Find(_ => true).ToListAsync();
        }

        // Retrieve a traveler by their unique ID.
        public async Task<Traveller> GetTravelerByIdAsync(string id)
        {
            return await _travelerCollection.Find(traveler => traveler.Id == id).FirstOrDefaultAsync();
        }

        // Create a new traveler and add them to the collection.
        public async Task<string> CreateTravelerAsync(Traveller traveler)
        {
            // Insert the new traveler into the MongoDB collection and return their ID.
            await _travelerCollection.InsertOneAsync(traveler);
            return traveler.Id;
        }

        // Update an existing traveler by their ID.
        public async Task<bool> UpdateTravelerAsync(string id, Traveller updatedTraveler)
        {
            // Replace the existing traveler document with the updated one.
            var updateResult = await _travelerCollection.ReplaceOneAsync(t => t.Id == id, updatedTraveler);
            return updateResult.ModifiedCount > 0; // Return true if the update was successful.
        }

        // Delete a traveler by their ID.
        public async Task<bool> DeleteTravelerAsync(string id)
        {
            // Delete the traveler document with the specified ID.
            var deleteResult = await _travelerCollection.DeleteOneAsync(traveler => traveler.Id == id);
            return deleteResult.DeletedCount > 0; // Return true if a document was deleted.
        }
    }
}
