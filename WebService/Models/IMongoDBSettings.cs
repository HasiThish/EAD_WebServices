namespace WebService.Models
{
    public interface IMongoDBSettings
    {
        string EmployeeCollection { get; set; } 

        string RoleCollection { get; set; } 

        string TrainCollection { get; set; }

        string TravellerCollection { get; set; }

        string BookingCollection { get; set; }

        string ConnectionString { get; set; } 

        string DatabaseName { get; set; } 
    }
}
