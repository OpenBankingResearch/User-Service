using UserAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserAPI.MongoRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUser(string id);

        Task AddUser(User user);

        Task<bool> RemoveUser(string id);

        Task<bool> UpdateUser(string id, string body);

        Task<bool> RemoveAllUsers();
    }
}
