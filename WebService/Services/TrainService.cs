using MongoDB.Bson;
using MongoDB.Driver;
using WebService.Models;

namespace WebService.Services
{
    public class TrainService : ITrainService
    {
        private readonly IMongoCollection<Train> _trainCollection;

        public TrainService(IMongoDBSettings settings, IMongoClient mongoClient)
        {
            // Initialize the MongoDB collection for trains using the provided settings.
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _trainCollection = database.GetCollection<Train>(settings.TrainCollection);
        }

        // Retrieve all trains from the MongoDB collection.
        public async Task<IEnumerable<Train>> GetAllTrainsAsync()
        {
            return await _trainCollection.Find(_ => true).ToListAsync();
        }

        // Retrieve a train by its unique ID.
        public async Task<Train> GetTrainByIdAsync(string id)
        {
            return await _trainCollection.Find(train => train.Id == id).FirstOrDefaultAsync();
        }

        // Add a new train to the collection.
        public async Task<string> AddTrainAsync(Train train)
        {
            await _trainCollection.InsertOneAsync(train);
            return train.Id;
        }

        // Update an existing train by its ID.
        public async Task<bool> UpdateTrainAsync(string id, Train train)
        {
            var updateResult = await _trainCollection.ReplaceOneAsync(t => t.Id == id, train);
            return updateResult.ModifiedCount > 0; // Return true if the update was successful.
        }

        // Delete a train by its ID.
        public async Task<bool> DeleteTrainAsync(string id)
        {
            var deleteResult = await _trainCollection.DeleteOneAsync(train => train.Id == id);
            return deleteResult.DeletedCount > 0; // Return true if a document was deleted.
        }

        // Retrieve the schedules of a train by its ID.
        public async Task<List<TrainSchedule>> GetTrainSchedulesByTrainIdAsync(string id)
        {
            var filter = await _trainCollection.Find(train => train.Id == id).FirstOrDefaultAsync();
            if (filter != null)
            {
                var schedule = filter.Schedule;
                return schedule;
            }
            else
            {
                // Handle the case when the train with the given ID is not found.
                return null; // You can return an empty list or handle the error as needed.
            }
        }

        // Add a schedule to a train by its ID.
        public async Task<bool> AddTrainScheduleAsync(string id, TrainSchedule schedule)
        {
            var filter = Builders<Train>.Filter.Eq(t => t.Id, id);
            var update = Builders<Train>.Update.Push(t => t.Schedule, schedule);

            var updateResult = await _trainCollection.UpdateOneAsync(filter, update);

            // Check if the update was successful and at least one document was modified.
            if (updateResult.ModifiedCount > 0)
            {
                return true;
            }
            else
            {
                // Handle the case when the train with the given ID is not found.
                return false; // Return false to indicate that the operation was not successful.
            }
        }

        // Update a train schedule by specifying train ID, schedule ID, and the updated schedule.
        public async Task<bool> UpdateTrainScheduleAsync(string trainId, string scheduleId, TrainSchedule updatedSchedule)
        {
            var filter = Builders<Train>.Filter.Where(t => t.Id == trainId && t.Schedule.Any(s => s.Id == scheduleId));
            var update = Builders<Train>.Update.Set("Schedule.$.Station", updatedSchedule.Station)
                .Set("Schedule.$.ArrivalTime", updatedSchedule.ArrivalTime)
                .Set("Schedule.$.DepartureTime", updatedSchedule.DepartureTime)
                .Set("Schedule.$.StopTime", updatedSchedule.StopTime);

            var updateResult = await _trainCollection.UpdateOneAsync(filter, update);
            return updateResult.ModifiedCount > 0;
        }

        // Delete a train schedule by specifying train ID and schedule ID.
        public async Task<bool> DeleteTrainScheduleAsync(string trainId, string scheduleId)
        {
            var filter = Builders<Train>.Filter.Where(t => t.Id == trainId && t.Schedule.Any(s => s.Id == scheduleId));
            var update = Builders<Train>.Update.PullFilter("Schedule", Builders<TrainSchedule>.Filter.Eq("_id", ObjectId.Parse(scheduleId)));

            var updateResult = await _trainCollection.UpdateOneAsync(filter, update);
            return updateResult.ModifiedCount > 0;
        }
    }
}
