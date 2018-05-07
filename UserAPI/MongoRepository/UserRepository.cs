using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace UserAPI.MongoRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext UserContext;

        public UserRepository(IOptions<Settings> options)
        {
            UserContext = new UserContext(options);
        }

        public Task AddUser(User User)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await UserContext.User.Find(x => true).ToListAsync();
        }

        public async Task<User> GetUser(string id)
        {
            ObjectId internalId;
            if (!ObjectId.TryParse(id, out internalId))
                internalId = ObjectId.Empty;
            var filter = Builders<User>.Filter.Eq(s => s._id, internalId);
            var result = await UserContext.User.FindAsync<User>(filter);
            return await result.FirstOrDefaultAsync(); ;
        }

        public Task<bool> RemoveAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUser(string id, string body)
        {
            throw new NotImplementedException();
        }
    }
}
