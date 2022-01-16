using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace MapFollow.Models
{
    public class RouteData
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public double X  { get; set; }

        public double Y { get; set; }

        public int Vehicle_Id { get; set; }
    }
}
