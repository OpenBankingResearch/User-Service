using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace UserAPI.Model
{
    public class User
    {
        [BsonId]
        public ObjectId _id { get; set; }

        public string userId { get; set; }

        public string jobTitleName { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string preferredName { get; set; }

        public string employeeCode { get; set; }

        public string region { get; set; }

        public string phoneNumber { get; set; }

        public string emailAddress { get; set; }

        public string Title { get; set; }

        public string Gender { get; set; }

        public string Password { get; set; }

    }
}
