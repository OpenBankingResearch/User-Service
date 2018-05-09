using CustomerAPI.Model;
using CustomerAPI.MongoRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository CustomerRepository;

        public CustomerController(ICustomerRepository CustomerRepository)
        {
            this.CustomerRepository = CustomerRepository;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await CustomerRepository.GetAllCustomers();
        }

        [HttpGet]
        public async Task<Customer> Get(string id)
        {
            return await CustomerRepository.GetCustomer(id);
        }
    }
}
