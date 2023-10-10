using MongoDB.Driver;
using WebService.Models;

namespace WebService.Services
{
    public class TrainService: ITrainService
    {
        private readonly IMongoCollection<Train> _trainCollection;

        public TrainService(IMongoDBSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _trainCollection = database.GetCollection<Train>(settings.TrainCollection);
        }
        
        public async Task<IEnumerable<Train>> GetAllTrainsAsync()
        {
            return await _trainCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Train> GetTrainByIdAsync(string id)
        {
            return await _trainCollection.Find(train => train.Id == id).FirstOrDefaultAsync();
        }

        public async Task<string> AddTrainAsync(Train train)
        {
            await _trainCollection.InsertOneAsync(train);
            return train.Id;
        }

        public async Task<bool> UpdateTrainAsync(string id, Train train)
        {
            var updateResult = await _trainCollection.ReplaceOneAsync(t => t.Id == id, train);
            return updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteTrainAsync(string id)
        {
            var deleteResult = await _trainCollection.DeleteOneAsync(train => train.Id == id);
            return deleteResult.DeletedCount > 0;
        }
    }
}
