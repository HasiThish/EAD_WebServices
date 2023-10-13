using WebService.Models;

namespace WebService.Services
{
    public interface ITrainService
    {
        Task<IEnumerable<Train>> GetAllTrainsAsync();
        Task<Train> GetTrainByIdAsync(string id);
        Task<string> AddTrainAsync(Train train);
        Task<bool> UpdateTrainAsync(string id, Train train);
        Task<bool> DeleteTrainAsync(string id);
        Task<List<TrainSchedule>> GetTrainSchedulesByTrainIdAsync(string id);
        Task<bool> AddTrainScheduleAsync(string id, TrainSchedule schedule);
        Task<bool> UpdateTrainScheduleAsync(string trainId, string scheduleId, TrainSchedule updatedSchedule);
        Task<bool> DeleteTrainScheduleAsync(string trainId, string scheduleId);
    }

}
