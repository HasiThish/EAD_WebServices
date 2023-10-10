namespace WebService.Models
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string EmployeeCollection { get; set; } = String.Empty;

        public string RoleCollection { get; set; } = String.Empty;

        public string TrainCollection { get; set; } = String.Empty;

        public string ConnectionString { get; set; } = String.Empty;

        public string DatabaseName { get; set; } = String.Empty;

    }
}
