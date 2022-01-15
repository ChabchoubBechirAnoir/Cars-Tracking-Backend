using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace MapFollow.Models
{
    public class RouteData
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public string Id { get; set; }

        public double X  { get; set; }

        public double Y { get; set; }

        public Guid Vehicle_Id { get; set; }
    }
}
