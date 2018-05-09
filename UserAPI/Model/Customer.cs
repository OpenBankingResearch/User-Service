using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CustomerAPI.Model
{
    [Serializable]
    [BsonIgnoreExtraElements]
    public class Customer
    {
        //[BsonId]
        //[BsonIgnore]
      //  public ObjectId _id { get; set; }

        public string customerId { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string gender { get; set; }

        public string email { get; set; }

        public string DOB { get; set; }

        public string state { get; set; }

        public string city { get; set; }

        public string postCode { get; set; }

        public string streetNumber { get; set; }

        public string country { get; set; }
    }
}
