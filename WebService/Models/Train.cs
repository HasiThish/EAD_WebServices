﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebService.Models
{
    public class Train
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("trainno")]
        public string TrainNumber { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("departure")]
        public string DepartureStation { get; set; }

        [BsonElement("arrival")]
        public string ArrivalStation { get; set; }

        [BsonElement("schedule")]
        public List<TrainSchedule> Schedule { get; set; } 

        [BsonElement("classes")]
        public List<string> Classes { get; set; }

        [BsonElement("days")]
        public List<string> OperatingDays { get; set; }
    }

    public class TrainSchedule
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("station")]
        public string Station { get; set; } = string.Empty;

        [BsonElement("arrival")]
        public string ArrivalTime { get; set; } = string.Empty;

        [BsonElement("departure")]
        public string DepartureTime { get; set; } = string.Empty;

        [BsonElement("stoptime")]
        public string StopTime { get; set; } = string.Empty;
    }
}
