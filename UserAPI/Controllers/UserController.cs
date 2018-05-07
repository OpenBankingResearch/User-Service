using UserAPI.Model;
using UserAPI.MongoRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserRepository UserRepository;

        public UserController(IUserRepository UserRepository)
        {
            this.UserRepository = UserRepository;
        }
        
        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await UserRepository.GetAllUsers();
        }

        [HttpGet]
        public async Task<User> Get(string id)
        {
            return await UserRepository.GetUser(id);
        }
    }
}
