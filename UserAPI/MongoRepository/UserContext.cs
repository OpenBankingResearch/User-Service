using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserAPI.Model;

namespace UserAPI.MongoRepository
{
    public class UserContext
    {
        private readonly IMongoDatabase mongoDatabases;

        public UserContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            mongoDatabases = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<User> User { get { return mongoDatabases.GetCollection<User>("user"); } }
    }
}
