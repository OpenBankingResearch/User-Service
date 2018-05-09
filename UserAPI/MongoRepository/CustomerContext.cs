using Microsoft.Extensions.Options;
using MongoDB.Driver;
using CustomerAPI.Model;

namespace CustomerAPI.MongoRepository
{
    public class CustomerContext
    {
        private readonly IMongoDatabase mongoDatabases;

        public CustomerContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            mongoDatabases = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Customer> Customer { get { return mongoDatabases.GetCollection<Customer>("customer"); } }
    }
}
